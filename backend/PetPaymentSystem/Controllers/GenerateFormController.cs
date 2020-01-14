using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.DTO;
using PetPaymentSystem.Models.Generated;
using PetPaymentSystem.Services;

namespace PetPaymentSystem.Controllers
{
    [Route("form/v1/[action]")]
    [ApiController]
    public class GenerateFormController : ControllerBase
    {
        [HttpGet]
        [Produces("text/html")]
        public ContentResult Pay(string Id,
            [FromServices] SessionManagerService sessionManager,
            [FromServices] OperationManagerService operationManager,
            [FromServices] FormManagerService formManager,
            [FromServices] PaymentSystemContext dbContext,
            [FromServices] FormDataCryptService cryptService)
        {

            try
            {
                var session = sessionManager.Get(Id);
                if (session.SessionType != SessionType.OneStep && session.SessionType != SessionType.TwoStep)
                    return base.Content(formManager.GetErrorForm());
                var result = operationManager.CheckPaymentPossibility(session);
                var dictionary = new Dictionary<string, string>();
                switch (result)
                {
                    case PaymentPossibility.LimitExceeded:
                    case PaymentPossibility.SessionExpired:
                        return base.Content(formManager.GetErrorForm());
                    case PaymentPossibility.AlreadyPaid:
                        return base.Content(formManager.GetSuccessForm());
                    default:
                        var generationTime = DateTime.UtcNow;
                        session.LastFormGenerationTime = generationTime;
                        session.TryCount++;
                        dbContext.SaveChanges();
                        var formSign = new FormSign { GenerationTime = generationTime, SessionId = session.Id };
                        dictionary.Add("sessionId", session.ExternalId);
                        dictionary.Add("code", cryptService.Crypt(formSign));
                        return base.Content(formManager.GetPaymentForm(dictionary));
                }
            }
            catch (Exception)
            {
                return base.Content(formManager.GetErrorForm());
            }

        }
    }
}