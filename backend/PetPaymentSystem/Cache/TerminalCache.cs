using System.Collections.Generic;
using PetPaymentSystem.Models.Generated;
using System.Linq;

namespace PetPaymentSystem.Cache
{
    public class TerminalCache : CommonCache<Terminal>
    {
        public static Terminal Get(int id, PaymentSystemContext context)
        {
            lock (LockObject)
            {
                Load(context);
                return _cache.FirstOrDefault(x => x.Id == id);
            }
        }

        public static IList<Terminal> All(PaymentSystemContext context)
        {
            lock (LockObject)
            {
                Load(context);
                return _cache.ToList();
            }
        }
        private static void Load(PaymentSystemContext context)
        {
            if (_loaded) return;
            _cache = context.Terminal.ToList();
            _loaded = true;
        }
    }
}
