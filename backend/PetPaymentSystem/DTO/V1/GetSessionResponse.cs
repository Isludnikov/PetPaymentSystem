namespace PetPaymentSystem.DTO.V1
{
    public class GetSessionResponse:CommonApiResponse
    {
        public string SessionId { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }
    }
}
