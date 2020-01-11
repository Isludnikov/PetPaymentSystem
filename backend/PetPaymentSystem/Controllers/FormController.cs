using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.Services;

namespace PetPaymentSystem.Controllers
{
    [Route("form/v1/[action]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        [HttpGet]
        [Produces("text/html")]
        public string Pay(string sessionId, [FromServices] SessionManagerService sessionManager, [FromServices] OperationManagerService operationManager)
        {
            var session = sessionManager.Get(sessionId);
            if (session == null) return "no session";
            /*var result = operationManager.CheckPaymentPossibility(session);
            if (!result) return */
            return string.Empty;
        }
    }
}