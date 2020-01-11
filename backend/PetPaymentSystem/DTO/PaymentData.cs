namespace PetPaymentSystem.DTO
{
    public class PaymentData
    {
        public string Pan { get; }
        public string Cvv { get; }

        public int? ExpireMonth { get; }

        public int? ExpireYear { get; }

        public PaymentData(string pan, int? expireYear = null, int? expireMonth = null, string cvv = null)
        {
            Pan = pan;
            ExpireYear = expireYear;
            ExpireMonth = expireMonth;
            Cvv = cvv;
        }
    }
}
