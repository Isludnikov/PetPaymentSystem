using System;
using System.Collections.Generic;

namespace PetPaymentSystem.models.generated
{
    public partial class Merchants
    {
        public Merchants()
        {
            MerchantIpRanges = new HashSet<MerchantIpRanges>();
        }

        public int Id { get; set; }
        public string Token { get; set; }
        public byte[] SignKey { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<MerchantIpRanges> MerchantIpRanges { get; set; }
    }
}
