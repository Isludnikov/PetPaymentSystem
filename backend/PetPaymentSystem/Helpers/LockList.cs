using System.Collections.Generic;

namespace PetPaymentSystem.Helpers
{
    public class LockList<T>
    {
        private readonly HashSet<T> _list;
        public LockList()
        {
            _list = new HashSet<T>();
        }

        public bool Add(T value) => _list.Add(value);
        public bool Remove(T value) => _list.Remove(value);
    }
}
