using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PetPaymentSystem.Constants;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Exceptions;
using PetPaymentSystem.Factories;
using PetPaymentSystem.Helpers;
using PetPaymentSystem.Library;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Services
{
    public class OperationManagerService
    {
        private readonly PaymentSystemContext _dbContext;
        private readonly ProcessingFactory _processingFactory;
        private readonly TerminalSelectorService _terminalSelector;
        private readonly ILogger<OperationManagerService> _logger;
        private readonly RemoteContainerService<PaymentData> _remoteContainer;

        public OperationManagerService(PaymentSystemContext dbContext,
            ProcessingFactory processingFactory,
            TerminalSelectorService terminalSelector,
            ILogger<OperationManagerService> logger,
            RemoteContainerService<PaymentData> remoteContainer)
        {
            _dbContext = dbContext;
            _processingFactory = processingFactory;
            _terminalSelector = terminalSelector;
            _logger = logger;
            _remoteContainer = remoteContainer;
        }

        public PaymentPossibility CheckPaymentPossibility(Session session)
        {
            if (session.Operation.Count == 0 && session.ExpireTime <= DateTime.UtcNow)
                return PaymentPossibility.SessionExpired;
            if (session.Operation.Any(x => x.OperationStatus == OperationStatus.Success))
                return PaymentPossibility.AlreadyPaid;
            if (session.TryCount >= GlobalConstants.MaxPaymentTriesCount)
                return PaymentPossibility.LimitExceeded;
            return PaymentPossibility.PaymentAllowed;

        }
        public PaymentPossibility CheckPaymentPossibility(Session session, Operation operation)
        {
            var lastOperation = session.Operation.OrderByDescending(x => x.Id).First();
            if (lastOperation.Id != operation.Id || lastOperation.OperationStatus != OperationStatus.AdditionalAuth)
                return PaymentPossibility.PaymentExpired;
            return PaymentPossibility.PaymentExpired;
        }
        public ProceedStatus Deposit(Merchant merchant, Session session, PaymentData paymentData, long amount = 0)
            => InnerSimpleOperation(merchant, session, paymentData, OperationType.Deposit, amount);

        public ProceedStatus Credit(Merchant merchant, Session session, PaymentData paymentData, long amount = 0)
            => InnerSimpleOperation(merchant, session, paymentData, OperationType.Credit, amount);

        public ProceedStatus Hold(Merchant merchant, Session session, PaymentData paymentData, long amount = 0)
            => InnerSimpleOperation(merchant, session, paymentData, OperationType.Hold, amount);

        public ProceedStatus Charge(Merchant merchant, Session session, PaymentData paymentData, long amount = 0)
            => InnerSimpleOperation(merchant, session, paymentData, OperationType.Charge, amount);

        private ProceedStatus InnerSimpleOperation(Merchant merchant, Session session, PaymentData paymentData, OperationType operationType, long amount)
        {
            var actualAmount = amount != 0 ? amount : session.Amount;
            var operationList = _dbContext.Operation.OrderByDescending(x => x.Id).Where(x => x.SessionId == session.Id).ToList();
            if (operationList.Count == 0 && session.ExpireTime < DateTime.Now)
                return new ProceedStatus { InnerError = InnerError.SessionExpired };
            var lastOperation = operationList.OrderByDescending(x => x.Id)
                .FirstOrDefault(x => x.OperationStatus == OperationStatus.Success);
            CheckPossibility(session, operationList, operationType, actualAmount);
            var terminal = _terminalSelector.Select(merchant, operationList, operationType, amount);

            var processing = _processingFactory.GetProcessing(terminal.Id, _dbContext);

            var operation = new Operation
            {
                SessionId = session.Id,
                OperationStatus = OperationStatus.Created,
                TerminalId = terminal.Id,
                Amount = actualAmount,
                InvolvedAmount = 0,
                ExternalId = IdHelper.GetOperationId(),
                OperationType = operationType,
                CreateDate = DateTime.UtcNow,
                ExpireMonth = paymentData?.ExpireMonth ?? lastOperation?.ExpireMonth,
                ExpireYear = paymentData?.ExpireYear ?? lastOperation?.ExpireYear,
                MaskedPan = paymentData != null ? MaskHelper.MaskPan(paymentData.Pan) : lastOperation?.MaskedPan
            };
            _dbContext.Operation.Add(operation);
            _dbContext.SaveChanges();
            ProceedStatus response;
            try
            {
                IProcessingResponse processingResponse;
                switch (operationType)
                {
                    case OperationType.Credit:
                        if (!processing.Properties.AllowCredit) throw new OuterException(InnerError.OperationNotSupportedByProcessing);
                        processingResponse = processing.Credit(session, operation, terminal, paymentData);
                        break;
                    case OperationType.Deposit:
                        if (!processing.Properties.AllowDebit) throw new OuterException(InnerError.OperationNotSupportedByProcessing);
                        processingResponse = processing.Debit(session, operation, terminal, paymentData);
                        break;
                    case OperationType.Hold:
                        if (!processing.Properties.AllowHold) throw new OuterException(InnerError.OperationNotSupportedByProcessing);
                        processingResponse = processing.Hold(session, operation, terminal, paymentData);
                        break;
                    case OperationType.Charge:
                        if (!processing.Properties.AllowHold || actualAmount != session.Amount && !processing.Properties.AllowPartialCharge) throw new OuterException(InnerError.OperationNotSupportedByProcessing);
                        processingResponse = processing.Charge(session, operation, terminal);
                        break;
                    default:
                        throw new OuterException(InnerError.NotImplemented);
                }

                operation.ProcessingOrderId = processingResponse.ProcessingOrderId;
                operation.OperationStatus = processingResponse.Status;
                AdditionalAuth auth = null;
                switch (processingResponse.Status)
                {
                    case OperationStatus.Success:
                        operation.InvolvedAmount = operation.Amount;
                        break;
                    case OperationStatus.AdditionalAuth:
                        auth = processingResponse.AuthData;
                        var operation3ds = new Operation3ds
                        {
                            LocalMd = IdHelper.GetMd(),
                            OperationId = operation.Id,
                            RemoteMd = auth.Md
                        };
                        _dbContext.Add(operation3ds);
                        _remoteContainer.Set(operation3ds.LocalMd, paymentData);
                        break;
                }
                response = new ProceedStatus { OperationStatus = operation.OperationStatus, AdditionalAuth = auth };
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                operation.OperationStatus = OperationStatus.Error;
                response = new ProceedStatus { OperationStatus = OperationStatus.Error };
            }
            finally
            {
                _dbContext.SaveChanges();
            }
            return response;
        }
        private void CheckPossibility(Session session, IList<Operation> operations, OperationType type, long amount)
        {
            switch (type)
            {
                case OperationType.Deposit:
                    if (session.SessionType != SessionType.OneStep || operations.Any(x => x.OperationStatus == OperationStatus.Success)) throw new OuterException(InnerError.PaymentAlreadyDone);
                    break;
                case OperationType.Hold:
                    if (session.SessionType != SessionType.TwoStep || operations.Any(x => x.OperationStatus == OperationStatus.Success)) throw new OuterException(InnerError.PaymentAlreadyDone);
                    break;
                case OperationType.Credit:
                    if (session.SessionType != SessionType.Credit || operations.Any(x => x.OperationStatus == OperationStatus.Success)) throw new OuterException(InnerError.PaymentAlreadyDone);
                    break;
                case OperationType.Charge:
                    if (session.SessionType != SessionType.TwoStep || operations.OrderByDescending(x => x.Id).First(x => x.OperationStatus == OperationStatus.Success).Amount < amount || operations.OrderByDescending(x => x.Id).First(x => x.OperationStatus == OperationStatus.Success).OperationType != OperationType.Hold) throw new OuterException(InnerError.PaymentAlreadyDone);
                    break;
                    //todo
            }
        }
    }
}
