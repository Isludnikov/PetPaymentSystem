using System;
using System.ComponentModel.DataAnnotations;

namespace PetPaymentSystem.Validators
{
    public class CardYearAttribute : ValidationAttribute
    {
        private const int MaxYearInterval = 50;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var now = DateTime.UtcNow;
            return value is int i && i >= now.Year && i <now.AddYears(MaxYearInterval).Year ? ValidationResult.Success : new ValidationResult($"Invalid Year-[{value}]");
        }
    }
}
