using PetPaymentSystem.Helpers;

namespace PetPaymentSystem.DTO
{
    public class ApiError
    {
        public string Code { get; }
        public string Message { get; }

        public string AdditionalInformation { get; set; }

        public ApiError(InnerError? error)
        {
            Code = error?.ToString()??"0";
            Message = OuterErrorMessageHelper.Get(error);
        }
    }
}
