using System;
using PetPaymentSystem.Library;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Factories
{
    public class ProcessingFactory
    {
        private static readonly object LockObject = new object();
        private static readonly Dictionary<int, IProcessing> Cache = new Dictionary<int, IProcessing>();

        public IProcessing GetProcessing(int id, PaymentSystemContext dbContext)
        {
            lock (LockObject)
            {
                if (!Cache.ContainsKey(id))
                {
                    var processingDb = dbContext.Processing.FirstOrDefault(x => x.Id == id);
                    if (processingDb == null) throw new Exception($"No processing with id-[{id}]");
                    Cache[id] = Create(processingDb);

                }
                return Cache[id];
            }
        }
        public void Clear()
        {
            lock (LockObject)
            {
                Cache.Clear();
            }
        }

        private IProcessing Create(Processing processing)
        {
            var dll = Assembly.LoadFile($".\\{processing.LibraryName}");

            var theType = dll.GetType(processing.Namespace);
            var instance = Activator.CreateInstance(theType);

            return (IProcessing) instance;
        }
    }
}
