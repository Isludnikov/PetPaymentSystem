using System;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Factories;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Services
{
    public class OperationManagerService
    {
        private readonly PaymentSystemContext _dbContext;
        private readonly ProcessingFactory _processingFactory;

        public OperationManagerService(PaymentSystemContext dbContext, ProcessingFactory processingFactory)
        {
            _dbContext = dbContext;
            _processingFactory = processingFactory;
        }

        public OperationStatus Proceed(Merchant merchant, Session session, OperationType type, long amount = 0)
        {
            switch (type)
            {
                case OperationType.Deposit:
                    return OperationStatus.Success;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
