using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Helpers;

namespace PetPaymentSystem.Services
{
    public class FormDataCryptService
    {
        private readonly string _secret;
        public FormDataCryptService(IConfiguration configuration)
        {
            _secret = configuration.GetSection("Keys").GetValue<string>("FormSignKey");
        }

        public string Crypt(FormSign formSign)
        {
            var plainData = JsonConvert.SerializeObject(formSign);
            return CryptHelper.Encrypt(plainData, _secret);
        }
        public FormSign DeCrypt(string secretText)
        {
            var plain = CryptHelper.Decrypt(secretText, _secret);
            return JsonConvert.DeserializeObject<FormSign>(plain);
        }
    }
}
