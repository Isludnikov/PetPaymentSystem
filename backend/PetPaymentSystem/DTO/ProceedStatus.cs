namespace PetPaymentSystem.DTO
{
    public class ProceedStatus
    {
        public OperationStatus OperationStatus { get; set; }
        public InnerError? InnerError { get; set; }
    }
}
