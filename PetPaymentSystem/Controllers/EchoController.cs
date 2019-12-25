using Microsoft.AspNetCore.Mvc;
using System;

namespace PetPaymentSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        [HttpPost]
        public string Echo()
        {
            return $"Echo {DateTime.Now}";
        }
    }
}