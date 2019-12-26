using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace PetPaymentSystem.Helpers
{
    public class HttpContextHelper
    {
        public static string GetBody(HttpRequest request)//https://devblogs.microsoft.com/aspnet/re-reading-asp-net-core-request-bodies-with-enablebuffering/
        {
            string bodyStr;

            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEndAsync().Result;
            }

            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyStr;
        }
    }
}
