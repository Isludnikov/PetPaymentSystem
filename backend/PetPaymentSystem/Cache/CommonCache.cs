using System.Collections.Generic;

namespace PetPaymentSystem.Cache
{
    public class CommonCache<T>
    {
        protected static bool _loaded;
        protected static readonly object LockObject;
        protected static List<T> _cache;

        static CommonCache()
        {
            _loaded = false;
            LockObject = new object();
            _cache = new List<T>();
        }
            
       public static void Clear()
        {
            lock (LockObject)
            {
                _cache.Clear();
                _loaded = false;
            }
        }
    }
}
