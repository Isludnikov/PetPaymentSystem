using PetPaymentSystem.DTO;
using PetPaymentSystem.Factories;
using System.Collections.Generic;
using System.Linq;

namespace PetPaymentSystem.Services
{
    public class FormManagerService
    {
        private readonly FormFactory _formFactory;
        public FormManagerService(FormFactory formFactory)
        {
            _formFactory = formFactory;
        }
        public string GetPendingForm(IDictionary<string, string> parameters = null, string key = "default")
        {
            var form = _formFactory.GetPendingForm(key);
            return Proceed(form, parameters ?? new Dictionary<string, string>());
        }
        public string GetSuccessForm(IDictionary<string, string> parameters = null, string key = "default")
        {
            var form = _formFactory.GetSuccessForm(key);
            return Proceed(form, parameters ?? new Dictionary<string, string>());
        }
        public string GetPaymentForm(IDictionary<string, string> parameters = null, string key = "default")
        {
            var form = _formFactory.GetPaymentForm(key);
            return Proceed(form, parameters ?? new Dictionary<string, string>());
        }
        public string GetErrorForm(IDictionary<string, string> parameters = null, string key = "default")
        {
            var form = _formFactory.GetErrorForm(key);
            return Proceed(form, parameters ?? new Dictionary<string, string>());
        }

        private string Proceed(Form form, IDictionary<string, string> parameters)
        {
            var result = parameters.Aggregate(form.Html, (current, param) => current.Replace($"[[{param.Key}]]", $"{param.Value}"));
            return result;
        }
    }
}
