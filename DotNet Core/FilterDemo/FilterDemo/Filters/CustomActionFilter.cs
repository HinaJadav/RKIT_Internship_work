using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        private readonly ILogger<CustomActionFilter> _logger;

        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var method = context.HttpContext.Request.Method;
            _logger.LogInformation($"Action '{actionName}' is executing with method '{method}' at {DateTime.Now}");
        }

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