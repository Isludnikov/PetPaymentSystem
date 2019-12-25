using System.Text.Json;

namespace PetPaymentSystem.DTO
{
    public class ApiResponse:IApiResponse
    {
        public virtual string GetLogString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
