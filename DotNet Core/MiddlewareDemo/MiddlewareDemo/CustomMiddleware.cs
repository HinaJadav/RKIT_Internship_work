namespace MiddlewareDemo
{
    public class CustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello from custom middleware part 1.\n");

            await next(context); // send context into next is mandatory into custom
                                 // 
            await context.Response.WriteAsync("hello from custom middleware part 2 after next()\n");

        }
    }
}
