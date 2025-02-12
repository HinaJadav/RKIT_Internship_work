namespace MiddlewareDemo
{
    /// <summary>
    /// Custom middleware implementing the IMiddleware interface.
    /// This middleware writes messages before and after passing the request to the next middleware in the pipeline.
    /// </summary>
    public class CustomMiddleware : IMiddleware
    {
        /// <summary>
        /// Handles the incoming HTTP request, executes the next middleware, and processes the response.
        /// </summary>
        /// <param name="context">Encapsulates all HTTP-specific information about the request.</param>
        /// <param name="next">Delegate that represents the next middleware in the pipeline.</param>
        /// <returns>A task that completes when the middleware execution is done.</returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Executed before calling the next middleware
            await context.Response.WriteAsync("Hello from custom middleware part 1.\n");

            // Pass control to the next middleware in the pipeline
            await next(context); // Mandatory to call the next middleware

            // Executed after the next middleware finishes processing
            await context.Response.WriteAsync("Hello from custom middleware part 2 after next().\n");
        }
    }
}
