using System;
using System.ComponentModel.DataAnnotations;
namespace PetPaymentSystem.Validators
{
    public class CardMonthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value is int i && i <= 12 && i > 0 ? ValidationResult.Success : new ValidationResult($"Invalid Month-[{value}]");
        }
    }
}
