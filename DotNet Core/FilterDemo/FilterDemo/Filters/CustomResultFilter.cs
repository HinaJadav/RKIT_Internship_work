using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    public class CustomResultFilter : IResultFilter
    {
        private readonly ILogger<CustomResultFilter> _logger;

        public CustomResultFilter(ILogger<CustomResultFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("Result is about to be executed at {Time}", DateTime.UtcNow);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("Result executed at {Time}", DateTime.UtcNow);

            if (context.Exception != null)
            {
                _logger.LogError("An exception occurred during result execution: {Message}", context.Exception.Message);
            }
        }
    }
}
