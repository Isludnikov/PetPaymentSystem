using PetPaymentSystem.DTO;

namespace PetPaymentSystem.Helpers
{
    public static class OuterErrorMessageHelper
    {
        public static string Get(InnerError? error)
        {
            var message = string.Empty;
            switch (error)
            {
                case InnerError.CommonError:
                    message = "General error";
                    break;
                case InnerError.SessionAlreadyExists:
                    message = "Duplicate OrderId";
                    break;
                case InnerError.ValidationError:
                    message = "Model validation error";
                    break;
            }

            return message;
        }
    }
}
