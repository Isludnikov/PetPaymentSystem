using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPaymentSystem.DTO;
using PetPaymentSystem.DTO.V1;
using PetPaymentSystem.Models.Generated;
using PetPaymentSystem.Services;

namespace PetPaymentSystem.Controllers
{
    [Route("form/v1/[action]")]
    [ApiController]
    public class ReturnFromFormController : ControllerBase
    {
        [HttpGet]
        [Produces("text/html")]
        public ContentResult Pay([FromForm] SubmitPay submitPay,
            [FromServices] OperationManagerService operationManager,
            [FromServices] FormManagerService formManager,
            [FromServices] PaymentSystemContext dbContext,
            [FromServices] FormDataCryptService cryptService)
        {
            if (string.IsNullOrEmpty(submitPay.ExternalId) || string.IsNullOrEmpty(submitPay.Code))
                return base.Content(formManager.GetErrorForm());
            var formCrypt = cryptService.DeCrypt(submitPay.Code);
            var session = dbContext.Session.Include(x => x.Merchant).FirstOrDefault(x => x.Id == formCrypt.SessionId);
            if (session == null || session.ExternalId != submitPay.ExternalId || session.ExpireTime != formCrypt.GenerationTime)
                return base.Content(formManager.GetErrorForm());

            var paymentData = new PaymentData(submitPay.Pan, submitPay.Year, submitPay.Month, submitPay.Cvv);

            var result = operationManager.Deposit(session.Merchant, session, paymentData);

            switch (result.OperationStatus)
            {
                case OperationStatus.AdditionalAuth:
                    return base.Content(formManager.Get3DsForm(result.AdditionalAuth));
                case OperationStatus.Pending:
                    return base.Content(formManager.GetPendingForm());
                case OperationStatus.Success:
                    return base.Content(formManager.GetSuccessForm());
                case OperationStatus.Redirected:
                    return base.Content(formManager.GetRedirectForm(result.RedirectedUrl));
                case OperationStatus.Error:
                    return session.CanTryToPayAnotherTime ? base.Content(formManager.GetRedirectForm("/form/v1/pay")) : base.Content(formManager.GetErrorForm());
                default:
                    return base.Content(formManager.GetErrorForm());
            }

        }
        [HttpGet]
        [Produces("text/html")]
        public ContentResult From3Ds([FromForm] Submit3Ds submit3Ds, [FromServices] SessionManagerService sessionManager,
            [FromServices] OperationManagerService operationManager,
            [FromServices] FormManagerService formManager,
            [FromServices] PaymentSystemContext dbContext,
            [FromServices] FormDataCryptService cryptService)
        {
            var operation3ds = dbContext.Operation3ds.Include(x => x.Operation).FirstOrDefault(x => x.LocalMd == submit3Ds.MD);
            if (operation3ds == null || operation3ds.Operation.OperationStatus != OperationStatus.AdditionalAuth) return base.Content(formManager.GetErrorForm());
            var session = dbContext.Session.Include(x => x.Operation).First(x => x.Id == operation3ds.Operation.SessionId);
            var possibility = operationManager.CheckPaymentPossibility(session, operation3ds.Operation);
            if (possibility != PaymentPossibility.PaymentAllowed) return base.Content(formManager.GetErrorForm());

            var result = operationManager.Deposit(session.Merchant, session, operation3ds);
            switch (result.OperationStatus)
            {
                case OperationStatus.Pending:
                    return base.Content(formManager.GetPendingForm());
                case OperationStatus.Success:
                    return base.Content(formManager.GetSuccessForm());
                case OperationStatus.Redirected:
                    return base.Content(formManager.GetRedirectForm(result.RedirectedUrl));
                case OperationStatus.Error:
                    return session.CanTryToPayAnotherTime ? base.Content(formManager.GetRedirectForm("/form/v1/pay")) : base.Content(formManager.GetErrorForm());
                default:
                    return base.Content(formManager.GetErrorForm());
            }
        }
    }
}