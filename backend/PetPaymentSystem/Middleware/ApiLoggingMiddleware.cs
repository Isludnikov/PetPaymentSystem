﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PetPaymentSystem.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PetPaymentSystem.Middleware
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiLoggingMiddleware> _logger;

        public ApiLoggingMiddleware(RequestDelegate next, ILogger<ApiLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.Value.StartsWith("/api/"))
                await _next(context);
            else
            {
                var logId = Guid.NewGuid().ToString();
                _logger.LogInformation($"REQUEST -[{logId}] body - [{HttpContextHelper.GetBody(context.Request)}]");

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
