using System.ComponentModel.DataAnnotations;

namespace PetPaymentSystem.Validators
{
    public class AmountAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value is long l && l > 0 ? ValidationResult.Success : new ValidationResult($"Invalid Amount-[{value}]");
        }
    }
}
