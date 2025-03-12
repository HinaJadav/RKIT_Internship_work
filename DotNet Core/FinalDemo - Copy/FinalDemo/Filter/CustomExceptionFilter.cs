using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinalDemo.Filter
{
    /// <summary>
    /// Custom exception filter to handle application-wide errors gracefully.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Unhandled exception occurred.");

            var statusCode = context.Exception switch
            {
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                InvalidOperationException =>HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var response = new
            {
                error = context.Exception.Message,
                statusCode = (int)statusCode
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}