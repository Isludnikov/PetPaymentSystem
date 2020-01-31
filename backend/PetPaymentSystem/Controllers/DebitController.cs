using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Models.Generated;
using PetPaymentSystem.Services;

namespace PetPaymentSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DebitController : ControllerBase
    {
        [HttpPost]
        public CommonApiResponse Debit(DebitRequest request, [FromServices] SessionManagerService sessionManager, [FromServices] OperationManagerService operationManager)
        {
            var merchant = (Merchant)HttpContext.Items["Merchant"];

            var session = sessionManager.Create(merchant, new SessionCreateRequest
            {
                Amount = request.Amount,
                Currency = request.Currency,
                OrderDescription = request.OrderDescription,
                OrderId = request.OrderId,
                SessionType = SessionType.OneStep
            });

            var paymentData = new PaymentData(request.Pan, request.Year, request.Month, request.Cvv);

            var result = operationManager.Deposit(merchant, session, paymentData);

            return new DebitResponse { Status = result.OperationStatus , Auth = result.AdditionalAuth};
        }
    }
}