using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Library
{
    public interface IProcessing
    {
        IProcessingResponse Debit(Session session, Operation operation);

        IProcessingResponse Credit(Session session, Operation operation);

        IProcessingProperties Properties { get; }
    }
}
