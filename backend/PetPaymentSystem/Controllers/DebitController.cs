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
        public CommonApiResponse Debit(DebitRequest request, [FromServices] SessionManagerService sessionManager)
        {
            var merchant = (Merchant)HttpContext.Items["Merchant"];

            var sessionCreateResponse = sessionManager.Create(merchant, new SessionCreateRequest
            {
                Amount = request.Amount,
                Currency = request.Currency,
                OrderDescription = request.OrderDescription,
                OrderId = request.OrderId,
            });

            if (sessionCreateResponse.Session == null || sessionCreateResponse.InnerError != null)
                return new CommonApiResponse
                {
                    Error = new ApiError(sessionCreateResponse.InnerError)
                };


            //select processing

            //start operation

            //call processing
            //write result to operation
            //return response
            return new DebitResponse { OrderId = sessionCreateResponse.Session.OrderId, Status = OperationStatus.Success };
        }
    }
}