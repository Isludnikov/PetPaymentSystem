namespace PetPaymentSystem.DTO
{
    public class ProceedStatus
    {
        public OperationStatus OperationStatus { get; set; }

        public AdditionalAuth AdditionalAuth { get; set; }
        public InnerError? InnerError { get; set; }

        public string RedirectedUrl { get; set; }
    }
}
