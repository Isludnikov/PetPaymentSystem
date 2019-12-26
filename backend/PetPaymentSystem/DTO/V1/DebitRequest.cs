
using System.ComponentModel.DataAnnotations;

namespace PetPaymentSystem.DTO.V1
{
    public class DebitRequest:IApiRequest
    {
        [Required]
        public string MerchantOrderId{ get; set; }
        [Required]
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
        public decimal Amount{ get; set; }

    }
}
