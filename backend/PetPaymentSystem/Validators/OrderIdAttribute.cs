using System.ComponentModel.DataAnnotations;

namespace PetPaymentSystem.Validators
{
    public class OrderIdAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           var stringValue = (string)value;
           return string.IsNullOrEmpty(stringValue) ? new ValidationResult("Empty OrderId not allowed") : ValidationResult.Success;
        }
    }
}
