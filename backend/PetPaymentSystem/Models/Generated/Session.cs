using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Models.Generated
{
    public partial class Session
    {
        public Session()
        {
            Operation = new HashSet<Operation>();
        }

        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string OrderId { get; set; }
        public int MerchantId { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string OrderDescription { get; set; }
        public string FormKey { get; set; }
        public string FormLanguage { get; set; }
        public DateTime ExpireTime { get; set; }
        public int TryCount { get; set; }
        public DateTime? LastFormGenerationTime { get; set; }

        public virtual Merchant Merchant { get; set; }
        public virtual ICollection<Operation> Operation { get; set; }
    }
}
