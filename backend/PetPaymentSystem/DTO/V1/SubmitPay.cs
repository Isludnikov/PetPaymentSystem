using System.ComponentModel.DataAnnotations;
using PetPaymentSystem.Validators;

namespace PetPaymentSystem.DTO.V1
{
    public class SubmitPay
    {
        [Required]
        [Pan]
        public string Pan { get; set; }
        [Required]
        [CardMonth]
        public int Month { get; set; }
        [Required]
        [CardYear]
        public int Year { get; set; }
        [Required]
        [Cvv]
        public string Cvv { get; set; }
        public string Code { get; set; }
        public string ExternalId { get; set; }
    }
}
