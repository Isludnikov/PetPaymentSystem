using PetPaymentSystem.DTO;
using System.ComponentModel.DataAnnotations;

namespace PetPaymentSystem.Validators
{
    public class SessionTypeAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value is SessionType ? ValidationResult.Success : new ValidationResult($"Invalid SessionType-[{value}]");
        }
    }
}
