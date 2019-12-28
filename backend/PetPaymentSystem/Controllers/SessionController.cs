using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Helpers;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpPost]
        public CommonApiResponse Start([FromBody] StartSessionRequest request,
            [FromServices] PaymentSystemContext dbContext)
        {
            var merchant = (Merchant) HttpContext.Items["Merchant"];
            if (dbContext.Session.Any(x => x.MerchantId == merchant.Id && x.OrderId == request.OrderId))
            {
                return new CommonApiResponse {Error = new ApiError {Code = "01", Message = "Duplicated OrderId"}};
            }

            var session = new Session
            {
                Amount = request.Amount,
                Currency = request.Currency,
                FormKey = request.FormKey,
                FormLanguage = request.FormLanguage,
                MerchantId = merchant.Id,
                OrderDescription = request.OrderDescription,
                OrderId = request.OrderId,
                ExternalId = IdHelper.GetSessionId()
            };

            dbContext.Session.Add(session);
            dbContext.SaveChanges();
            return new StartSessionResponse{SessionId = session.ExternalId};
        }
    }
}