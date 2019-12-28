using System.ComponentModel.DataAnnotations;
using PetPaymentSystem.Validators;

namespace PetPaymentSystem.DTO.V1
{
    public class StartSessionRequest
    {
        [Required]
        public string OrderId { get; set; }
        [Required]
        [Currency]
        public string Currency{ get; set; }
        [Required]
        public long Amount{ get; set; }
        public string OrderDescription { get; set; }
        public string FormKey { get; set; }
        [Language]
        public string FormLanguage { get; set; }
    }
}
