using Microsoft.Extensions.Configuration;
using PetPaymentSystem.Cache;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Exceptions;
using PetPaymentSystem.Models.Generated;
using System.Collections.Generic;
using System.Linq;

namespace PetPaymentSystem.Services
{
    public class TerminalSelectorService
    {
        private readonly PaymentSystemContext _dbContext;
        private readonly bool _useCache;
        public TerminalSelectorService(PaymentSystemContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _useCache = configuration.GetSection("Caching").GetValue<bool>("Terminals");
        }

        public Terminal Select(Merchant merchant, IEnumerable<Operation> operationList, OperationType type, long amount = 0)
        {
            var terminals = _useCache ? TerminalCache.All(_dbContext) : _dbContext.Terminal.Where(x => x.MerchantId == merchant.Id && x.Active).ToList();
            if (!terminals.Any())
                throw new OuterException(InnerError.TerminalNotConfigured, $"No configured terminals for merchant id-[{merchant.Id}] name-[{merchant.ShortName}]");
            var lastNotDeclinedOperation = operationList.OrderByDescending(x => x.Id).FirstOrDefault(x => x.OperationStatus != OperationStatus.Declined);
            if (lastNotDeclinedOperation != null)
            {
                var selectedTerminal = _useCache ? TerminalCache.Get(lastNotDeclinedOperation.TerminalId, _dbContext) : terminals.FirstOrDefault(x => x.Id == lastNotDeclinedOperation.TerminalId);
                if (selectedTerminal == null)
                    throw new OuterException(InnerError.TerminalBlocked, $"Terminal not found for session id[{lastNotDeclinedOperation.SessionId}]");
                return selectedTerminal;
            }
            // todo code for routing here
            return terminals.FirstOrDefault();
        }

        public Terminal Select(int id)
        {
            var terminals = _useCache ? TerminalCache.All(_dbContext) : _dbContext.Terminal.Where(x => x.Id == id && x.Active).ToList();
            var selectedTerminal = _useCache ? TerminalCache.Get(id, _dbContext) : terminals.FirstOrDefault(x => x.Id == id);

            return selectedTerminal;
        }
    }
}
