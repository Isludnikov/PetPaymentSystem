using System;
using System.Collections.Generic;
using System.Linq;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Factories;
using PetPaymentSystem.Helpers;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Services
{
    public class OperationManagerService
    {
        private readonly PaymentSystemContext _dbContext;
        private readonly ProcessingFactory _processingFactory;
        private readonly TerminalSelectorService _terminalSelector;

        public OperationManagerService(PaymentSystemContext dbContext, ProcessingFactory processingFactory, TerminalSelectorService terminalSelector)
        {
            _dbContext = dbContext;
            _processingFactory = processingFactory;
            _terminalSelector = terminalSelector;
        }

        public ProceedStatus Proceed(Merchant merchant, Session session, OperationType type, long amount = 0)
        {
            var Amount = amount != 0 ? amount : session.Amount;
            var operationList = _dbContext.Operation.OrderByDescending(x=>x.Id).Where(x => x.SessionId == session.Id).ToList();
            if (operationList.Count == 0 && session.ExpireTime < DateTime.Now)
                return new ProceedStatus { InnerError = InnerError.SessionExpired };

            CheckPossibility(session, operationList, type, Amount);
            var terminal = _terminalSelector.Select(merchant, operationList, type, amount);

            var processing = _processingFactory.GetProcessing(terminal, _dbContext);

            var operation = new Operation
            {
                SessionId = session.Id,
                Status = (int)OperationStatus.Created,
                TerminalId = terminal.Id,
                Amount = Amount,
                InvolvedAmount = 0,
                ExternalId = IdHelper.GetOperationId(),
                OperationType = (int)type,
                CreateDate = DateTime.UtcNow
            };
            _dbContext.Operation.Add(operation);
            _dbContext.SaveChanges();

            switch (type)
            {
                case OperationType.Deposit:

                    var processingResponse = processing.Debit(session, operation);
                    operation.ProcessingOrderId = processingResponse.ProcessingOrderId;
                    operation.Status = (int)processingResponse.Status;
                    switch (processingResponse.Status)
                    {
                        case OperationStatus.Success:
                            operation.InvolvedAmount = operation.Amount;
                            break;

                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            _dbContext.SaveChanges();

            var response = new ProceedStatus { OperationStatus = (OperationStatus)operation.Status };

            return response;
        }

        private void CheckPossibility(Session session, IEnumerable<Operation> operations, OperationType type, long amount)
        {
            switch (type)
            {
                case OperationType.Deposit:
                case OperationType.Hold:
                    if (operations.Any(x=>x.Status == (int)OperationStatus.Success)) throw new Exception("");
                    break;
                //case OperationType.Charge

            }
        }
    }
}
