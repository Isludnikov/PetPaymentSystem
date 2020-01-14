using PetPaymentSystem.Constants;
using PetPaymentSystem.DTO;

namespace PetPaymentSystem.Models.Generated
{
    public partial class Session
    {
        public SessionType SessionType { get; set; }

        public bool CanTryToPayAnotherTime =>
            (Merchant?.MaxTriesToPay ?? TryCount) <= GlobalConstants.MaxPaymentTriesCount;
    }
    
}
