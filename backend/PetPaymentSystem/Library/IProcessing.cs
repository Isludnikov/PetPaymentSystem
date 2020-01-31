using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Library
{
    public interface IProcessing
    {
        IProcessingResponse Debit(Session session, Operation operation, Terminal terminal, PaymentData paymentData);

        IProcessingResponse Credit(Session session, Operation operation, Terminal terminal, PaymentData paymentData);

        IProcessingResponse Hold(Session session, Operation operation, Terminal terminal, PaymentData paymentData);

        IProcessingResponse Charge(Session session, Operation operation, Terminal terminal);

        IProcessingResponse Process3Ds(Session session, Operation operation, Terminal terminal, PaymentData paymentData, Submit3Ds submit3Ds);

        IProcessingProperties Properties { get; }
    }
}
