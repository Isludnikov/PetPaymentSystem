using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Models.Generated
{
    public partial class MerchantIpRange
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public string Iprange { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
