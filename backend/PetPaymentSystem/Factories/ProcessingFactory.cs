using PetPaymentSystem.Library;
using PetPaymentSystem.Models.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PetPaymentSystem.Factories
{
    public class ProcessingFactory
    {
        private static readonly object LockObject = new object();
        private static readonly Dictionary<int, IProcessing> Cache = new Dictionary<int, IProcessing>();

        public IProcessing GetProcessing(Terminal terminal, PaymentSystemContext dbContext)
        {
            lock (LockObject)
            {
                if (!Cache.ContainsKey(terminal.Id))
                {
                    var processingDb = dbContext.Processing.FirstOrDefault(x => x.Id == terminal.Id);
                    if (processingDb == null) throw new Exception($"No processing with id-[{terminal.Id}]");
                    Cache[terminal.Id] = Create(processingDb, terminal);

                }
                return Cache[terminal.Id];
            }
        }
        public void Clear()
        {
            lock (LockObject)
            {
                Cache.Clear();
            }
        }

        private IProcessing Create(Processing processing, Terminal terminal)
        {
            var pathToAssembly = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var filename = $"{pathToAssembly}\\{processing.LibraryName}";
            var dll = Assembly.LoadFile(filename);

            var theType = dll.GetType(processing.Namespace);
            var instance = Activator.CreateInstance(theType, terminal);

            return (IProcessing) instance;
        }
    }
}
