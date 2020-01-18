using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Services
{
    public class RemoteContainerService<T>
    {
        private static Dictionary<string,string> dataDictionary = new Dictionary<string, string>(); //todo в будущем заменить на сервер key=>value
        private static object lockObject = new object();

        public T Get(string key)
        {
            lock (lockObject)
            {
                if (!dataDictionary.TryGetValue(key, out var value)) return default;
                dataDictionary.Remove(key);
                return JsonConvert.DeserializeObject<T>(value);
            }
        }

        public bool Set(string key, T value)
        {
            lock (lockObject)
            {
                try
                {
                    dataDictionary.Add(key, JsonConvert.SerializeObject(value));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
