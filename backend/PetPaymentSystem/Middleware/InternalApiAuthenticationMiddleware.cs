using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetPaymentSystem.Constants;
using PetPaymentSystem.Helpers.IpSet;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PetPaymentSystem.Middleware
{
    public class InternalApiAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<InternalApiAuthenticationMiddleware> _logger;
        private readonly IConfiguration _configuration;
        private const string LocalIps = "127.0.0.0/24, ::1";

        public InternalApiAuthenticationMiddleware(RequestDelegate next, ILogger<InternalApiAuthenticationMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
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
            if (!context.Request.Path.Value.StartsWith("/iapi/")) return true;

            if (context.Request.Headers[GlobalConstants.KeyHeader].Count == 0)
            {
                _logger.LogWarning("Required header is missing");
                return false;
            }

            var token = context.Request.Headers[GlobalConstants.KeyHeader][0];

            if (token != _configuration.GetSection("Keys").GetValue<string>("InternalApiKey"))
                return false;

            var ip = context.Connection.RemoteIpAddress.ToString();
            var set = IpSet.ParseOrDefault(LocalIps);
            if (!set.Contains(ip))
            {
                _logger.LogWarning($"Ip [{ip}] not allowed");
                return false;
            }

            return true;
        }
    }
}
