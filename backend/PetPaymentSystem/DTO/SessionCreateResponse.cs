using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.DTO
{
    public class SessionCreateResponse
    {
        public Session Session { get; set; }

        public InnerError? InnerError { get; set; }
    }
}
