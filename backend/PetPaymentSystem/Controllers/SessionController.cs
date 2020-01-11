using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Exceptions;
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

            var session = sessionManager.Create(merchant, new SessionCreateRequest
            {
                Amount = request.Amount,
                Currency = request.Currency,
                FormKey = request.FormKey,
                FormLanguage = request.FormLanguage,
                OrderDescription = request.OrderDescription,
                OrderId = request.OrderId,
                SessionType = request.SessionType
            });

            return new StartSessionResponse { SessionId = session.ExternalId };
        }

        public CommonApiResponse Get([FromBody] GetSessionRequest request, [FromServices] SessionManagerService sessionManager)
        {
            var merchant = (Merchant)HttpContext.Items["Merchant"];

            var session = sessionManager.Get(request.SessionId);

            if (session == null || session.MerchantId != merchant.Id)
                throw new OuterException(InnerError.SessionNotFound);

            return new GetSessionResponse { SessionId = session.ExternalId, Amount = session.Amount, Currency = session.Currency };
        }
    }
}