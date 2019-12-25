using System.Text.Json;

namespace PetPaymentSystem.DTO
{
    public class ApiRequest:IApiRequest
    {
        public virtual string GetLogString()
        {
            return JsonSerializer.Serialize(this.Mask());
        }

        public virtual ApiRequest Mask()
        {
            return this;
        }
    }
}
