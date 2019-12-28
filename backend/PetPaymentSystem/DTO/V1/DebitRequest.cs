
using System.ComponentModel.DataAnnotations;
using PetPaymentSystem.Validators;

namespace PetPaymentSystem.DTO.V1
{
    public class DebitRequest:IApiRequest
    {
        [Required]
        public string OrderId{ get; set; }
        [Required]
        [Currency]
        public string Currency{ get; set; }
        [Required]
        public string Pan { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Cvv { get; set; }
        [Required]
        public long Amount{ get; set; }
        public string OrderDescription { get; set; }

    }
}
