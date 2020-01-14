using PetPaymentSystem.DTO;
using System.Collections.Generic;
using System.IO;

namespace PetPaymentSystem.Factories
{
    public class FormFactory
    {
        private static readonly object LockObject = new object();
        private static readonly Dictionary<string, Form> Cache = new Dictionary<string, Form>();
        public Form GetPendingForm(string key="default") => GetForm("pending", key);
        public Form GetSuccessForm(string key="default") => GetForm("success", key);
        public Form GetErrorForm(string key="default") => GetForm("error", key);
        public Form GetPaymentForm(string key="default") => GetForm("payment", key);
        private Form GetForm(string type, string key)
        {
            var compositeKey = type + key;
            lock (LockObject)
            {
                if (!Cache.ContainsKey(compositeKey))
                {
                    var pathToAssembly = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                    var form = new Form {Key = key, Type = type};
                    var filename = pathToAssembly+$"\\forms\\{type}-{key}-form.html";
                    form.Html = File.ReadAllText(filename);
                    
                    Cache[compositeKey] = form;

                }
                return Cache[compositeKey];
            }
        }
        public static void Clear()
        {
            lock (LockObject)
            {
                Cache.Clear();
            }
        }
    }
}
