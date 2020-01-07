using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Cache
{
    public static class MerchantCache
    {
        private static bool _loaded;
        private static readonly object LockObject;
        private static List<Merchant> _cache;

        static MerchantCache()
        {
            _loaded = false;
            LockObject = new object();
            _cache = new List<Merchant>();
        }
        public static Merchant GetMerchant(string token, PaymentSystemContext context)
        {
            lock (LockObject)
            {
                LoadCache(context);
                return _cache.FirstOrDefault(x => x.Token == token);
            }
        }
        public static Merchant GetMerchant(int id, PaymentSystemContext context)
        {
            lock (LockObject)
            {
                LoadCache(context);

                return _cache.FirstOrDefault(x => x.Id == id);
            }
        }

        public static void ClearCache()
        {
            lock (LockObject)
            {
                _cache.Clear();
                _loaded = false;
            }
        }

        private static void LoadCache(PaymentSystemContext context)
        {
            if (!_loaded)
            {
                _cache = context.Merchant.Include(i => i.MerchantIpRange).ToList();
                _loaded = true;
            }
        }
    }
}
