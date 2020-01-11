using PetPaymentSystem.DTO;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Library
{
    public interface IProcessing
    {
        IProcessingResponse Debit(Session session, Operation operation, Terminal terminal, PaymentData paymentData);

        IProcessingResponse Credit(Session session, Operation operation, Terminal terminal, PaymentData paymentData);

        IProcessingResponse Hold(Session session, Operation operation, Terminal terminal, PaymentData paymentData);

        IProcessingResponse Charge(Session session, Operation operation, Terminal terminal);

        IProcessingProperties Properties { get; }
    }
}
