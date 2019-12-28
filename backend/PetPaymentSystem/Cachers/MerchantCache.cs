using System;
using PetPaymentSystem.Models.Generated;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PetPaymentSystem.Cachers
{
    public class MerchantCache
    {
        private static bool loaded;
        private static object lockObject;
        private static List<Merchant> cache;

        static MerchantCache()
        {
            loaded = false;
            lockObject = new object();
            cache = new List<Merchant>();

            /*var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(10);

            var timer = new System.Threading.Timer(e =>
            {
                ClearCache();   
            }, null, startTimeSpan, periodTimeSpan);*/
        }
        public static Merchant GetMerchant(string token, PaymentSystemContext context)
        {
            lock (lockObject)
            {
                if (!loaded)
                {
                   LoadCache(context);
                }
                return cache.FirstOrDefault(x => x.Token == token);
            }
        }
        public static Merchant GetMerchant(int id, PaymentSystemContext context)
        {
            lock (lockObject)
            {
                if (!loaded)
                {
                    LoadCache(context);
                }
                return cache.FirstOrDefault(x => x.Id == id);
            }
        }

        public static void ClearCache()
        {
            lock (lockObject)
            {
                cache.Clear();
                loaded = false;
            }
        }

        private static void LoadCache(PaymentSystemContext context)
        {
            cache = context.Merchant.Include(i=>i.MerchantIpRange).ToList();
            loaded = true;
        }
    }
}
