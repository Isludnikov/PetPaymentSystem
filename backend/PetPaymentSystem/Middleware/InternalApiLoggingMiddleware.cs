using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PetPaymentSystem.Constants;
using PetPaymentSystem.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PetPaymentSystem.Middleware
{
    public class InternalApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiLoggingMiddleware> _logger;

        public InternalApiLoggingMiddleware(RequestDelegate next, ILogger<ApiLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.Value.StartsWith("/iapi/"))
                await _next(context);
            else
            {
                var logId = Guid.NewGuid().ToString();

                var key = context.Request.Headers[GlobalConstants.KeyHeader];
                _logger.LogInformation($"REQUEST - [{logId}] key - [{MaskHelper.MaskHeader(key)}] body - [{HttpContextHelper.GetBody(context.Request)}]");

                string responseContent;

                var originalBodyStream = context.Response.Body;
                await using (var fakeResponseBody = new MemoryStream())
                {
                    context.Response.Body = fakeResponseBody;

                    await _next(context);

                    fakeResponseBody.Seek(0, SeekOrigin.Begin);
                    using var reader = new StreamReader(fakeResponseBody);
                    responseContent = await reader.ReadToEndAsync();
                    fakeResponseBody.Seek(0, SeekOrigin.Begin);

                    await fakeResponseBody.CopyToAsync(originalBodyStream);
                }

                _logger.LogInformation($"RESPONSE -[{logId}] body - [{responseContent}]");
            }
        }
    }
}
