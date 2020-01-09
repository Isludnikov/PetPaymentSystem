using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using PetPaymentSystem.Helpers;

namespace PetPaymentSystem.Validators
{
    public class CvvAttribute : ValidationAttribute
    {
        private const string Pattern = @"^\d{3,4}";
        private static readonly Regex CvvRegex = new Regex(Pattern, RegexOptions.Compiled);
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValue = (string)value;
            return !string.IsNullOrEmpty(stringValue) && CvvRegex.IsMatch(stringValue) ? ValidationResult.Success : new ValidationResult($"Invalid Cvv-[{MaskHelper.MaskCvv(stringValue)}]");
        }
    }
}
