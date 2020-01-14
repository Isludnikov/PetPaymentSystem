using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.Cache;
using PetPaymentSystem.Factories;

namespace PetPaymentSystem.Controllers
{
    [Route("iapi/[action]")]
    [ApiController]
    public class InternalController : ControllerBase
    {
        [HttpPost]
        public bool ClearCache()
        {
            MerchantCache.Clear();
            TerminalCache.Clear();
            ProcessingFactory.Clear();
            FormFactory.Clear();
            return true;
        }
    }
}