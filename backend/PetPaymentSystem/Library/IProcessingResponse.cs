using PetPaymentSystem.DTO;

namespace PetPaymentSystem.Library
{
    public interface IProcessingResponse
    {
        OperationStatus Status { get; set; }

        string ProcessingOrderId { get; set; }

        AdditionalAuth AuthData { get; set; }

        bool SavePaymentData { get; set; }
    }
}
