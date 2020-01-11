using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Models.Generated;
using PetPaymentSystem.Services;

namespace PetPaymentSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        [HttpPost]
        public CommonApiResponse Credit(CreditRequest request, [FromServices] SessionManagerService sessionManager, [FromServices] OperationManagerService operationManager)
        {
            var merchant = (Merchant)HttpContext.Items["Merchant"];

            var session = sessionManager.Create(merchant, new SessionCreateRequest
            {
                Amount = request.Amount,
                Currency = request.Currency,
                OrderDescription = request.OrderDescription,
                OrderId = request.OrderId,
                SessionType = SessionType.Credit
            });

            var paymentData = new PaymentData(request.Pan, request.Year, request.Month);

            var result = operationManager.Credit(merchant, session, paymentData);

            return new CreditResponse { Status = result.OperationStatus };
        }
    }
}