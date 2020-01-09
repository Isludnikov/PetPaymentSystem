using System;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Library;
using PetPaymentSystem.Models.Generated;

namespace TestProcessing
{
    public class TestProcessing : IProcessing
    {
        private readonly Terminal _terminal;
        public TestProcessing(Terminal terminal)
        {
            _terminal = terminal;
        }
        public IProcessingResponse Debit(Session session, Operation operation)
        {
            return new ProcessingResponse
            { ProcessingOrderId = Guid.NewGuid().ToString(), Status = OperationStatus.Success };
        }

        public IProcessingResponse Credit(Session session, Operation operation)
        {
            return new ProcessingResponse
            { ProcessingOrderId = Guid.NewGuid().ToString(), Status = OperationStatus.Success };
        }

        public IProcessingProperties Properties
            => new ProcessingProperties(true, true, true, true, true, true);
    }
}
