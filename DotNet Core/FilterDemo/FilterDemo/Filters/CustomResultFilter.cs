using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    /// <summary>
    /// Custom result filter that logs details before and after the execution of an action result.
    /// Useful for monitoring and debugging the response process.
    /// </summary>
    public class CustomResultFilter : IResultFilter
    {
        private readonly ILogger<CustomResultFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomResultFilter"/> class.
        /// </summary>
        /// <param name="logger">Logger instance used for logging result execution details.</param>
        public CustomResultFilter(ILogger<CustomResultFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called before the action result executes.
        /// Logs when the result is about to be executed.
        /// </summary>
        /// <param name="context">The result executing context.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("Result is about to be executed at {Time}", DateTime.UtcNow);
        }

        /// <summary>
        /// Called after the action result has executed.
        /// Logs when the result execution is completed and captures any exceptions if they occurred.
        /// </summary>
        /// <param name="context">The result executed context.</param>
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
