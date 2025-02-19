using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    /// <summary>
    /// Custom exception filter that handles unhandled exceptions globally.
    /// Logs exceptions and returns a standardized error response.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExceptionFilter"/> class.
        /// </summary>
        /// <param name="logger">Logger instance used for logging exception details.</param>
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called when an unhandled exception occurs during request processing.
        /// Logs the exception and modifies the response to return a generic error message.
        /// </summary>
        /// <param name="context">The exception context.</param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogError("An exception occurred: {Message}", context.Exception.Message);

            // Customize the response when an exception occurs
            context.Result = new ObjectResult(new { message = "An unexpected error occurred." })
            {
                StatusCode = StatusCodes.Status500InternalServerError // Internal Server Error
            };

            // Mark exception as handled
            context.ExceptionHandled = true;
        }
    }
}
