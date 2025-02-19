using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    /// <summary>
    /// Custom action filter that logs details about action execution before and after it runs.
    /// Helps in tracking action execution flow and handling exceptions.
    /// </summary>
    public class CustomActionFilter : IActionFilter
    {
        private readonly ILogger<CustomActionFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActionFilter"/> class.
        /// </summary>
        /// <param name="logger">Logger instance used for logging action execution details.</param>
        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called before the action method executes.
        /// Logs the action name and HTTP method.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var method = context.HttpContext.Request.Method;
            _logger.LogInformation($"Action '{actionName}' is executing with method '{method}' at {DateTime.Now}");
        }

        /// <summary>
        /// Called after the action method executes.
        /// Logs the action execution result, including errors if any occurred.
        /// </summary>
        /// <param name="context">The action executed context.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                _logger.LogError($"Action executed with an error: {context.Exception.Message}");
            }
            else
            {
                var actionName = context.ActionDescriptor.DisplayName;
                _logger.LogInformation($"Action '{actionName}' executed successfully at {DateTime.Now}");
            }
        }
    }
}
