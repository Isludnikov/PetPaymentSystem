using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Models.Generated;
using PetPaymentSystem.Services;

namespace PetPaymentSystem.Controllers
{
    [Route("api/v1/[action]")]
    [ApiController]
    public class ReturnFromFormController : ControllerBase
    {
        [HttpPost]
        [Produces("text/html")]
        public ContentResult Pay([FromForm] SubmitPay submitPay, [FromServices] SessionManagerService sessionManager,
            [FromServices] OperationManagerService operationManager,
            [FromServices] FormManagerService formManager,
            [FromServices] PaymentSystemContext dbContext,
            [FromServices] FormDataCryptService cryptService)
        {
            if (string.IsNullOrEmpty(submitPay.ExternalId) || string.IsNullOrEmpty(submitPay.Code))
                return base.Content(formManager.GetErrorForm());
            var formCrypt = cryptService.DeCrypt(submitPay.Code);
            var session = dbContext.Session.Include(x => x.MerchantId).FirstOrDefault(x => x.Id == formCrypt.SessionId);
            if (session == null || session.ExternalId != submitPay.ExternalId || session.ExpireTime != formCrypt.GenerationTime)
                return base.Content(formManager.GetErrorForm());


            var paymentData = new PaymentData(submitPay.Pan, submitPay.Year, submitPay.Month, submitPay.Cvv);

            var result = operationManager.Deposit(session.Merchant, session, paymentData);

            switch (result.OperationStatus)
            {
                case OperationStatus.AdditionalAuth:
                    return base.Content(formManager.Get3DSForm());
                case OperationStatus.Pending:
                    return base.Content(formManager.GetPendingForm());
                case OperationStatus.Success:
                    return base.Content(formManager.GetSuccessForm());
                case OperationStatus.Redirected:
                    return base.Content(formManager.GetRedirectForm());
                case OperationStatus.Error:
                    return session.CanTryToPayAnotherTime ? base.Content(formManager.GetRedirectForm()) : base.Content(formManager.GetErrorForm());
                default:
                    return base.Content(formManager.GetErrorForm());
            }

        }
    }
}