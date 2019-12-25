using System;
using System.Collections.Generic;

namespace PetPaymentSystem.models.generated
{
    public partial class MerchantIpRanges
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public string Iprange { get; set; }

        public virtual Merchants Merchant { get; set; }
    }
}
