using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    public class CustomExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("An exception occurred: {Message}", context.Exception.Message);

            // Customize the response when an exception occurs
            context.Result = new ObjectResult(new { message = "An unexpected error occurred." })
            {
                StatusCode = 500 // Internal Server Error
            };
        }
    }
}
