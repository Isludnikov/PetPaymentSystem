using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace PetPaymentSystem.Helpers
{
    public class HttpContextHelper
    {
        public static string GetBody(HttpContext context)//https://devblogs.microsoft.com/aspnet/re-reading-asp-net-core-request-bodies-with-enablebuffering/
        {
            string bodyStr;

            context.Request.EnableBuffering();

            // Arguments: Stream, Encoding, detect encoding, buffer size 
            // AND, the most important: keep stream opened so next middleware can read it
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEndAsync().Result;
            }

            // Rewind, so the core is not lost when it looks the body for the request
            context.Request.Body.Position = 0;

            return bodyStr;
        }
    }
}
