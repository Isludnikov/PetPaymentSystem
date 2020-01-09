using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using PetPaymentSystem.Helpers;

namespace PetPaymentSystem.Validators
{
    public class PanAttribute : ValidationAttribute
    {
        private const string Pattern = @"^\d{16,19}";
        private static readonly Regex PanRegex = new Regex(Pattern, RegexOptions.Compiled);
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValue = (string)value;
            return !string.IsNullOrEmpty(stringValue) && PanRegex.IsMatch(stringValue) ? ValidationResult.Success : new ValidationResult($"Invalid PAN-[{MaskHelper.MaskPan(stringValue)}]");
        }
    }
}
