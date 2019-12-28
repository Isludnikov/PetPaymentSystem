using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.DTO.V1;
using System;
using System.Linq;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Helpers;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DebitController : ControllerBase
    {
        [HttpPost]
        public CommonApiResponse Debit(DebitRequest request, [FromServices] PaymentSystemContext dbContext)
        {
            //todo
            //check merchant orderId uniqueness
            var merchant = (Merchant)HttpContext.Items["Merchant"];
            if (dbContext.Session.Any(x => x.MerchantId == merchant.Id && x.OrderId == request.OrderId))
            {
                return new CommonApiResponse { Error = new ApiError { Code = "01", Message = "Duplicated OrderId" } };
            }

            //start session
            var session = new Session
            {
                Amount = request.Amount,
                Currency = request.Currency,
                FormKey = null,
                FormLanguage = "RUS",
                MerchantId = merchant.Id,
                OrderDescription = request.OrderDescription,
                OrderId = request.OrderId,
                ExternalId = IdHelper.GetSessionId()
            };

            dbContext.Session.Add(session);
            dbContext.SaveChanges();
            //select processing

            //start operation

            //call processing
            //write result to operation
            //return response
            return new DebitResponse { OrderId = Guid.NewGuid().ToString(), Status = DepositStatus.Success };
        }
    }
}