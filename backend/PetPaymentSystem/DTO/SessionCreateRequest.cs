namespace PetPaymentSystem.DTO
{
    public class SessionCreateRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string FormKey { get; set; }
        public string FormLanguage { get; set; } = "RUS";
        public string OrderDescription { get; set; }
        public string OrderId { get; set; }
        public SessionType SessionType { get; set; }
    }
}
