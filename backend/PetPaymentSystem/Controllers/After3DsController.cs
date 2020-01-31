using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Models.Generated;
using PetPaymentSystem.Services;

namespace PetPaymentSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class After3DsController : ControllerBase
    {
        [HttpPost]
        public CommonApiResponse After3Ds(Submit3Ds submit3Ds,
            [FromServices] SessionManagerService sessionManager,
            [FromServices] OperationManagerService operationManager,
            [FromServices] PaymentSystemContext dbContext)
        {
            var merchant = (Merchant)HttpContext.Items["Merchant"];

            var operation3ds = dbContext.Operation3ds.Include(x => x.Operation).FirstOrDefault(x => x.LocalMd == submit3Ds.MD);
            if (operation3ds == null || operation3ds.Operation.OperationStatus != OperationStatus.AdditionalAuth)
                return new DebitResponse { Error = new ApiError(InnerError.CommonError) };

            var session = dbContext.Session.Include(x => x.Operation).First(x => x.Id == operation3ds.Operation.SessionId);
            if (session.MerchantId != merchant.Id) 
                return new DebitResponse { Error = new ApiError(InnerError.CommonError) };

            var possibility = operationManager.CheckPaymentPossibility(session, operation3ds.Operation);
            if (possibility != PaymentPossibility.PaymentAllowed)
                return new DebitResponse { Error = new ApiError(InnerError.CommonError) };

            var result = operationManager.Deposit(session, operation3ds, submit3Ds);
            return new DebitResponse { Status = result.OperationStatus };
        }

    }
}