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
    public class ChargeController : ControllerBase
    {
        [HttpPost]
        public CommonApiResponse Hold(ChargeRequest request, [FromServices] SessionManagerService sessionManager, [FromServices] OperationManagerService operationManager)
        {
            var merchant = (Merchant)HttpContext.Items["Merchant"];

            var session = sessionManager.GetByOrderId(merchant, request.OrderId);

            if (session == null)
                return new CommonApiResponse
                {
                    Error = new ApiError(InnerError.SessionNotFound)
                };

            var result = operationManager.Charge(merchant, session, null, request.Amount);
            
            return new DebitResponse { Status = result.OperationStatus, Auth = result.AdditionalAuth};
        }
    }
}