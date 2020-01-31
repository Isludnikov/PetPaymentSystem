using PetPaymentSystem.DTO;
using PetPaymentSystem.Library;

namespace TestProcessing
{
    class ProcessingResponse:IProcessingResponse
    {
        public OperationStatus Status { get; set; }
        public string ProcessingOrderId { get; set; }
        public AdditionalAuth AuthData { get; set; }
        public bool SavePaymentData { get; set; }
    }
}
