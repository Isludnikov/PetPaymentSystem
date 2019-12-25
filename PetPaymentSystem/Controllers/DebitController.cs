using Microsoft.AspNetCore.Mvc;
using PetPaymentSystem.DTO.V1;

namespace PetPaymentSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DebitController : ControllerBase
    {
        [HttpPost]
        public DebitResponse Debit(DebitRequest request)
        {
            //todo
            return new DebitResponse();
        }
    }
}