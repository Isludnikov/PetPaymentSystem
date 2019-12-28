using System.ComponentModel.DataAnnotations;
using System.Linq;
using PetPaymentSystem.Helpers;

namespace PetPaymentSystem.Validators
{
    public class LanguageAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var language = (string)value;
            return LanguageHelper.GetLanguages().Any(x=>x.Iso639_3==language) ? ValidationResult.Success : new ValidationResult($"Invalid language code [{language}]");
        }
    }
}
