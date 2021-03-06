﻿using Microsoft.EntityFrameworkCore;
using PetPaymentSystem.Models.Generated;
using System.Linq;

namespace PetPaymentSystem.Cache
{
    public class MerchantCache:CommonCache<Merchant>
    {
        public static Merchant Get(string token, PaymentSystemContext context)
        {
            lock (LockObject)
            {
                Load(context);
                return _cache.FirstOrDefault(x => x.Token == token);
            }
        }
        public static Merchant Get(int id, PaymentSystemContext context)
        {
            lock (LockObject)
            {
                Load(context);

                return _cache.FirstOrDefault(x => x.Id == id);
            }
        }

        private static void Load(PaymentSystemContext context)
        {
            if (_loaded) return;
            _cache = context.Merchant.Include(i => i.MerchantIpRange).ToList();
            _loaded = true;
        }
    }
}
