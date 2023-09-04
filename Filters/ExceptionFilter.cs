using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Blog.API.Models;

namespace Blog.API.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is DomainException ex)
            {
                context.Result = BuildErrorObject(ex.StatusCode, ex.Errors);
                return;
            }

            context.Result = BuildErrorObject(HttpStatusCode.InternalServerError, context.Exception.Message, context.Exception.StackTrace);
        }

        private JsonResult BuildErrorObject(HttpStatusCode statusCode, string message, string stacktrace)
        {
            dynamic objectError = new
            {
                message,
                timestamp = DateTime.UtcNow,
                stacktrace
            };

            return new JsonResult(objectError)
            {
                StatusCode = (int)statusCode
            };
        }

        private JsonResult BuildErrorObject(HttpStatusCode statusCode, List<string> errors)
        {
            dynamic error = new
            {
                message = errors,
                timestamp = DateTime.UtcNow
            };

            return new JsonResult(error)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
