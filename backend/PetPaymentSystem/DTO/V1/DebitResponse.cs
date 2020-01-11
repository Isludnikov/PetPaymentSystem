using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PetPaymentSystem.DTO.V1
{
    public class DebitResponse:CommonApiResponse
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationStatus Status{ get; set; }

        public AdditionalAuth Auth { get; set; }
    }
}
