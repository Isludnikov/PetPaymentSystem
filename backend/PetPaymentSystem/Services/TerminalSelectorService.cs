using PetPaymentSystem.DTO;
using PetPaymentSystem.Models.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using PetPaymentSystem.Exceptions;

namespace PetPaymentSystem.Services
{
    public class TerminalSelectorService
    {
        private readonly PaymentSystemContext _dbContext;

        public TerminalSelectorService(PaymentSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Terminal Select(Merchant merchant, IEnumerable<Operation> operationList, OperationType type, long amount = 0)
        {
            var terminals = _dbContext.Terminal.Where(x => x.MerchantId == merchant.Id && x.Active).ToList();
            if (terminals.Count == 0)
                throw new OuterException(InnerError.TerminalNotConfigured, $"No configured terminals for merchant id-[{merchant.Id}] name-[{merchant.ShortName}]");
            var lastNotDeclinedOperation = operationList.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Status != (int)OperationStatus.Declined);
            if (lastNotDeclinedOperation != null)
            {
                var selectedTerminal = terminals.FirstOrDefault(x => x.Id == lastNotDeclinedOperation.TerminalId);
                if (selectedTerminal == null) 
                    throw new OuterException(InnerError.TerminalBlocked, $"Terminal not found for session id[{lastNotDeclinedOperation.SessionId}]");
                return selectedTerminal;
            }
            // todo code for routing here
            return terminals.FirstOrDefault();
        }
    }
}
