using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PetPaymentSystem.Validators;

namespace PetPaymentSystem.DTO.V1
{
    public class DebitRequest : IApiRequest, IValidatableObject
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
        [CardYear]
        [Required]
        public int Year { get; set; }
        [CardMonth]
        [Required]
        public int Month { get; set; }
        [Cvv]
        [Required]
        public string Cvv { get; set; }
        [Amount]
        [Required]
        public long Amount { get; set; }
        public string OrderDescription { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var now = DateTime.UtcNow;
            if (now.Month < Month && now.Year == Year)
            {
                yield return new ValidationResult(
                    $"Card lifetime already expired at {Month:D2}/{Year}.",
                    new[] { nameof(Month) });
            }
        }

    }
}
