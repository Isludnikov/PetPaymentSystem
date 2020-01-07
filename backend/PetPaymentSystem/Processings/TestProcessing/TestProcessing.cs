using System;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Library;
using PetPaymentSystem.Models.Generated;

namespace TestProcessing
{
    public class TestProcessing : IProcessing
    {
        public IProcessingResponse Debit(Session session, Operation operation)
        {
            return new ProcessingResponse
                {ProcessingOrderId = Guid.NewGuid().ToString(), Status = OperationStatus.Success};
        }

        public IProcessingResponse Credit(Session session, Operation operation)
        {
            return new ProcessingResponse
                {ProcessingOrderId = Guid.NewGuid().ToString(), Status = OperationStatus.Success};
        }
    }
}
