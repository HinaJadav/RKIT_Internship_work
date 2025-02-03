using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    public class CustomResourceFilter : IResourceFilter
    {
        // This method is executed before the action method is executed
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // Adding a custom header before the resource is executed
            context.HttpContext.Response.Headers.Add("X-Custom-Header", "My custom header value");
        }

        // This method is executed after the action method has executed
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // No additional action needed after resource execution in this simple version
        }
    }
}