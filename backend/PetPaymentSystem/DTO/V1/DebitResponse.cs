namespace PetPaymentSystem.DTO.V1
{
    public class DebitResponse:CommonApiResponse
    {
        public string OrderId{ get; set; }

        public OperationStatus Status{ get; set; }
    }
}
