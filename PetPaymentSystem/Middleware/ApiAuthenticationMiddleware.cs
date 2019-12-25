using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using PetPaymentSystem.Helpers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PetPaymentSystem.models.generated;

namespace PetPaymentSystem.Middleware
{
    public class ApiAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiAuthenticationMiddleware> _logger;
        private readonly PaymentSystemContext _dbContext;

        private const string AuthHeader = "auth";
        private const string SignHeader = "sign";

        public ApiAuthenticationMiddleware(RequestDelegate next, ILogger<ApiAuthenticationMiddleware> logger, PaymentSystemContext dbContext)
        {
            _next = next;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (Check(context))
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden");
            }
        }

        private bool Check(HttpContext context)
        {
            if (!context.Request.Path.Value.StartsWith("/api/")) return true;

            if (context.Request.Headers[AuthHeader].Count == 0 ||
                context.Request.Headers[SignHeader].Count == 0)
            {
                _logger.LogWarning("Required headers are missing");
                return false;
            }

            var token = context.Request.Headers[AuthHeader][0];
            var merchant = _dbContext.Merchants.FirstOrDefault(x => x.Token == token);
            if (merchant == null) return false;
            //todo check token and get merchant

            var sign = context.Request.Headers[SignHeader][0];
            var body = HttpContextHelper.GetBody(context);

            //todo check sign

            var ip = context.Connection.RemoteIpAddress.ToString();

            //todo check ip

            return true;
        }
    }
}
