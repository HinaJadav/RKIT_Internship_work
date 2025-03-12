namespace FinalDemo.Middleware
{
    /// <summary>
    /// Middleware to log incoming requests and outgoing responses.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation($"[Request] {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");

                await _next(context); // Call the next middleware

                _logger.LogInformation($"[Response] {context.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Error] An unhandled exception occurred.");
                throw; // Re-throw the exception to preserve existing behavior
            }
        }
    }
}
