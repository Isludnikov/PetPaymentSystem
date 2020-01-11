using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetPaymentSystem.DTO;

namespace PetPaymentSystem.Helpers
{
    public static class ModelValidationHelper
    {
        public static CommonApiResponse Validate(ModelStateDictionary modelStates)
        {
            var response = new ApiError(InnerError.ValidationError);
            foreach (var modelState in modelStates)
            {
                response.AdditionalInformation += $"{modelState.Key}-";
                foreach (var error in modelState.Value.Errors)
                {
                    response.AdditionalInformation += error.ErrorMessage;
                }

                response.AdditionalInformation += ";";
            }
            return new CommonApiResponse
            {
                Error = response
            };
        }
    }
}
