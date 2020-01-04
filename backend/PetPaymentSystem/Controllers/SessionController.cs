using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Models.Generated;
using PetPaymentSystem.Services;

namespace PetPaymentSystem.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpPost]
        public CommonApiResponse Start([FromBody] StartSessionRequest request, [FromServices] SessionManagerService sessionManager)
        {
            var merchant = (Merchant)HttpContext.Items["Merchant"];

            var sessionCreateResponse = sessionManager.Create(merchant, new SessionCreateRequest
            {
                Amount = request.Amount,
                Currency = request.Currency,
                FormKey = request.FormKey,
                FormLanguage = request.FormLanguage,
                OrderDescription = request.OrderDescription,
                OrderId = request.OrderId,
            });

            if (sessionCreateResponse.Session != null && sessionCreateResponse.InnerError == null)
                return new StartSessionResponse { SessionId = sessionCreateResponse.Session.ExternalId };

            return new CommonApiResponse
            {
                Error = new ApiError(sessionCreateResponse.InnerError),
            };
        }
    }
}