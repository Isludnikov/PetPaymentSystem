using PetPaymentSystem.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetPaymentSystem.DTO.V1
{
    public class CreditRequest : IApiRequest, IValidatableObject
    {
        [OrderId]
        [Required]
        public string OrderId { get; set; }
        [Required]
        [Currency]
        public string Currency { get; set; }
        [Pan]
        [Required]
        public string Pan { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        [Required]
        [Amount]
        public long Amount { get; set; }
        public string OrderDescription { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Month == null ^ Year == null) yield return new ValidationResult(
                "Both of ExpireYear and ExpireMonth must be set or unset/null",
                new[] { nameof(Year) , nameof(Month)});

                var now = DateTime.UtcNow;

            if (Year != null && Year < now.Year && Year > now.Year + 50) yield return new ValidationResult(
                        $"Bad ExpireYear [{Year}]",
                        new[] { nameof(Year) });

            if (Month != null && Year != null && now.Month < Month && now.Year == Year)

                yield return new ValidationResult(
                    $"Card lifetime already expired at {Month:D2}/{Year}.",
                    new[] { nameof(Month) });
        }
    }
}
