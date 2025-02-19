using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;
using System.Net;

namespace RoutingDemo
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configures services for Dependency Injection.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add Swagger services
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the middleware pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Swagger middleware
            app.UseSwagger();

            // Enable Swagger UI middleware
            app.UseSwaggerUI();

            var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();

            /// <summary>
            /// UseWhen Middleware:
            /// - A middleware branching method that conditionally executes middleware based on a specific condition.
            /// - Useful for selectively applying middleware logic based on the request.
            /// - Helps keep the middleware pipeline clean by only applying logic where necessary.
            /// 
            /// Middleware Details:
            /// - This middleware activates when the request path starts with "/api/useWhen".
            /// - Logs an informational message when the route is accessed.
            /// - Returns a response: "UseWhen Middleware Executed".
            /// </summary>
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/useWhen"), appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    logger.LogInformation("UseWhen Middleware Executed");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{ \"message\": \"UseWhen Middleware Executed!\" }");
                    await next(); // Allows request to continue
                });
            });

            /// <summary>
            /// MapWhen() Middleware:
            /// - Executes middleware only if the request path matches the condition.
            /// - If the request matches, it does NOT continue to other middlewares or routing.
            /// - Example: A request to "/api/mapWhen" triggers this middleware, and further processing stops.
            /// </summary>
            app.MapWhen(context => context.Request.Path.StartsWithSegments("/api/mapWhen"), appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    logger.LogInformation("MapWhen Middleware Executed");

                    // Set response type to JSON (for Swagger visibility)
                    context.Response.ContentType = "application/json";

                    // Responds with a message and stops request processing
                    await context.Response.WriteAsync("{ \"message\": \"MapWhen Middleware Executed!\" }");

                    // No `next()` call, so request processing stops here
                });
            });

            /// <summary>
            /// Global Middleware:
            /// - Runs for all requests that are NOT handled by MapWhen().
            /// - Logs request information before and after processing.
            /// - Calls 'next()' to allow the request to continue.
            /// </summary>
            app.Use(async (context, next) =>
            {
                logger.LogInformation($"Request received: {context.Request.Path}");

                await next(); // Allows the request to move to the next middleware

                logger.LogInformation($"Response sent: {context.Request.Path}");
            });



            app.UseRouting();

            // Routing : 
            app.UseEndpoints(endpoints =>
            {
                // 1) Conventional routing :
                // use: Into controller based app with multiple action methods
                // # Enable comments for Conventional routing  


                /*// Default route
                // http://localhost:25800/
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Custom route for 'About' section
                // http://localhost:25800/about-us
                endpoints.MapControllerRoute(
                    name: "aboutRoute",
                    pattern: "about-us",
                    defaults: new { controller = "Home", action = "About" });*/


                // # Disable below line during Conventional routing

                // 2) Attribute routing:
                // use: Need more control over individual routes and routes don't follow a common pattern

                endpoints.MapControllers();


 
                // # route handlers(Minimal API routing) : here use as endpoint methods 
                // we can also use them as middleware 
                // Use: lightweight APIs without controllers, for MicroServices and for Minimal APIs
                // work as Simple endpoints for apis


                // a) Simple GET request
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello, World!");
                });

                // b) Route with parameter
                endpoints.MapGet("/hello/{name}", async context =>
                {
                    var name = context.Request.RouteValues["name"]?.ToString();
                    await context.Response.WriteAsync($"Hello, {name}!");
                });

                // c) Route with query parameters
                endpoints.MapGet("/add", async context =>
                {
                    var query = context.Request.Query;
                    if (query.ContainsKey("a") && query.ContainsKey("b"))
                    {
                        int a = int.Parse(query["a"]);
                        int b = int.Parse(query["b"]);
                        await context.Response.WriteAsync($"Sum: {a + b}");
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Please provide 'a' and 'b' as query parameters.");
                    }
                });

                // d) POST request handling
                endpoints.MapPost("/submit", async context =>
                {
                    using var reader = new StreamReader(context.Request.Body);
                    var content = await reader.ReadToEndAsync();
                    await context.Response.WriteAsync($"Received: {content}");
                });
            

                endpoints.MapGet("/api/useWhen/test", async context =>
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{ \"message\": \"Final Response for UseWhen!\" }");
                });

                endpoints.MapGet("/api/mapWhen/test", async context =>
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{ \"message\": \"Final Response for MapWhen!\" }");
                });

            });

        }
    }
}
// what is difference between filter and middleware 
/*.NET Core Routing

Uses Microsoft.AspNetCore.Routing.
Middleware-based (UseRouting, UseEndpoints).
Attribute routing is primary.
More performant and flexible.
Supports advanced route constraints and endpoint filters.*/