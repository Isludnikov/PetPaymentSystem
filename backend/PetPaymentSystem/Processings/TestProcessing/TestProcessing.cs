using System;
using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Library;
using PetPaymentSystem.Models.Generated;

namespace TestProcessing
{
    public class TestProcessing : IProcessing
    {
        public IProcessingResponse Debit(Session session, Operation operation, Terminal terminal, PaymentData paymentData)
        {
            return new ProcessingResponse
            { ProcessingOrderId = Guid.NewGuid().ToString(), Status = OperationStatus.Success };
        }

        public IProcessingResponse Credit(Session session, Operation operation, Terminal terminal, PaymentData paymentData)
        {
            return new ProcessingResponse
            { ProcessingOrderId = Guid.NewGuid().ToString(), Status = OperationStatus.Success };
        }

        public IProcessingResponse Hold(Session session, Operation operation, Terminal terminal, PaymentData paymentData)
        {
            return new ProcessingResponse
                { ProcessingOrderId = Guid.NewGuid().ToString(), Status = OperationStatus.Success };
        }

        public IProcessingResponse Charge(Session session, Operation operation, Terminal terminal)
        {
            return new ProcessingResponse
                { ProcessingOrderId = Guid.NewGuid().ToString(), Status = OperationStatus.Success };
        }

        public IProcessingResponse Process3Ds(Session session, Operation operation, Terminal terminal, PaymentData paymentData, Submit3Ds submit3Ds)
        {
            return new ProcessingResponse
                { ProcessingOrderId = Guid.NewGuid().ToString(), Status = OperationStatus.Success };
        }

        public IProcessingProperties Properties
            => new ProcessingProperties(true, true, false, true, false, false);
    }
}
