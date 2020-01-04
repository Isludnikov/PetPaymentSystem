using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetPaymentSystem.DTO;

namespace PetPaymentSystem.Filter
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null && context.HttpContext.Request.Path.Value.StartsWith("/api"))
            {
                context.Result = new ObjectResult(new CommonApiResponse
                {
                    Error = new ApiError(InnerError.CommonError)
                });
                context.ExceptionHandled = true;
            }
        }
    }
}
