namespace PetPaymentSystem.DTO.V1
{
    public class DebitResponse
    {
        public string OrderId{ get; set; }

        public DepositStatus Status{ get; set; }
    }
}
