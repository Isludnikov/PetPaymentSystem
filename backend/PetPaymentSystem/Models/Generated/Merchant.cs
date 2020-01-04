﻿using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Models.Generated
{
    public partial class Merchant
    {
        public Merchant()
        {
            MerchantIpRange = new HashSet<MerchantIpRange>();
            Session = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Token { get; set; }
        public string SignKey { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<MerchantIpRange> MerchantIpRange { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}