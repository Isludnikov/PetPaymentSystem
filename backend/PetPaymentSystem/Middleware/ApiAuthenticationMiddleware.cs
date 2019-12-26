using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using PetPaymentSystem.Helpers;
using PetPaymentSystem.Helpers.IpSet;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PetPaymentSystem.Constants;
using PetPaymentSystem.Models.Generated;

namespace PetPaymentSystem.Middleware
{
    public class ApiAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiAuthenticationMiddleware> _logger;
        private readonly PaymentSystemContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ApiAuthenticationMiddleware(RequestDelegate next, ILogger<ApiAuthenticationMiddleware> logger, DbContextPool<PaymentSystemContext> dbContext, IConfiguration configuration, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _dbContext = DbContextExtractor.Extract(dbContext);
            _configuration = configuration;
            _env = env;
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

            if (context.Request.Headers[GlobalConstants.AuthHeader].Count == 0 ||
                context.Request.Headers[GlobalConstants.SignHeader].Count == 0)
            {
                _logger.LogWarning("Required headers are missing");
                return false;
            }

            var token = context.Request.Headers[GlobalConstants.AuthHeader][0];
            var merchant = _dbContext.Merchants.Include(i=>i.MerchantIpRanges).FirstOrDefault(x => x.Token == token);
            if (merchant == null)
            {
                _logger.LogWarning("No merchant with token");
                return false;
            }

            if (!_env.IsDevelopment() || _configuration.GetSection("DebugFlags").GetValue<bool>("CheckSign"))
            {
                var sign = context.Request.Headers[GlobalConstants.SignHeader][0];
                var body = HttpContextHelper.GetBody(context.Request);
                using var mySha256 = SHA256.Create();
                var calculatedSign =
                    Convert.ToBase64String(mySha256.ComputeHash(Encoding.UTF8.GetBytes(body + merchant.SignKey)));
                if (sign != calculatedSign)
                {
                    _logger.LogWarning("Bad sign");
                    return false;
                }
            }

            if (!_env.IsDevelopment() || _configuration.GetSection("DebugFlags").GetValue<bool>("CheckIP"))
            {
                if (merchant.MerchantIpRanges.Count != 0)
                {
                    var ip = context.Connection.RemoteIpAddress.ToString();
                    var set = IpSet.ParseOrDefault(merchant.MerchantIpRanges.Select(x => x.Iprange));
                    if (!set.Contains(ip))
                    {
                        _logger.LogWarning($"Ip [{ip}] not allowed");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
