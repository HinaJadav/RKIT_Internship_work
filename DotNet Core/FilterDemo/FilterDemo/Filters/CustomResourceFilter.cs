using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    /// <summary>
    /// Custom resource filter that runs before and after the execution of an action method.
    /// Used to modify the request or response at the resource level.
    /// </summary>
    public class CustomResourceFilter : IResourceFilter
    {
        /// <summary>
        /// Called before the action method executes.
        /// Adds a custom header to the response before the resource is processed.
        /// </summary>
        /// <param name="context">The resource executing context.</param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // Adding a custom header before the resource is executed
            context.HttpContext.Response.Headers.Add("X-Custom-Header", "My custom header value");
        }

        /// <summary>
        /// Called after the action method has executed.
        /// Can be used for post-processing logic (not implemented in this version).
        /// </summary>
        /// <param name="context">The resource executed context.</param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // No additional action needed after resource execution in this simple version
        }
    }
}
