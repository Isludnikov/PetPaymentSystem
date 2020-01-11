namespace PetPaymentSystem.DTO.V1
{
    public class ChargeRequest
    {
        public string OrderId { get; set; }

        public long Amount { get; set; }
    }
}
