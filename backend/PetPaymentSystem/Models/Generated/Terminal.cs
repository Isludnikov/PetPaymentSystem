using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Models.Generated
{
    public partial class Terminal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcessingId { get; set; }
        public int Priority { get; set; }
        public int MerchantId { get; set; }
        public string Rule { get; set; }
        public bool FinalRule { get; set; }
        public bool NextOnError { get; set; }

        public virtual Merchant Merchant { get; set; }
        public virtual Processing Processing { get; set; }
    }
}
