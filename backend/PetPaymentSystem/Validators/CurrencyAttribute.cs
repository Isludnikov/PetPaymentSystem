using System.ComponentModel.DataAnnotations;
using System.Linq;
using PetPaymentSystem.Helpers;

namespace PetPaymentSystem.Validators
{
    public class CurrencyAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValue = (string) value;
            return CurrencyHelper.GetCurrencies().Any(x => x.Alfa3 == stringValue) ? ValidationResult.Success : new ValidationResult($"Invalid currency code [{stringValue}]");
        }
    }
}
