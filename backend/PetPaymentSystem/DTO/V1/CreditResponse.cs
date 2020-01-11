namespace PetPaymentSystem.DTO.V1
{
    public class CreditResponse:CommonApiResponse
    {
        public string OrderId{ get; set; }
        public OperationStatus Status{ get; set; }
    }
}
