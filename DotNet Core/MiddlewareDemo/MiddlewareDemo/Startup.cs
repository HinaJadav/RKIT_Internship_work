using Microsoft.Extensions.FileProviders;

namespace MiddlewareDemo
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
        /// <param name="services">The service collection to configure services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Registers support for controllers in the application.
            services.AddControllers();

            // Registers a custom middleware for use in the application.
            services.AddTransient<CustomMiddleware>();

            // Registers Swagger services for API documentation.
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the middleware pipeline that handles HTTP requests and responses.
        /// </summary>
        /// <param name="app">The application builder for configuring middleware.</param>
        /// <param name="env">The hosting environment providing enviro nment-specific details.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Built-in middleware
            // Middleware order is follow for execution of middleware pipeline.

            /// <summary>
            /// Adds middleware to handle runtime exceptions in development.
            /// </summary>
            if (env.IsDevelopment())
            {
                /// <summary>
                /// Handles exceptions pages during development.
                /// </summary>
                app.UseDeveloperExceptionPage();
            }
            else
            {
                /// <summary>
                /// Handles exceptions in production by redirecting to a custom error page.
                /// </summary>
                app.UseExceptionHandler("/Error");

                /// <summary>
                /// Enforces HTTP Strict Transport Security (HSTS) to enhance security.
                /// Enforces strict HTTPS on;y connections.
                /// </summary>
                app.UseHsts();
            }

            /// <summary>
            /// Redirects HTTP requests to HTTPS for secure communication.
            /// </summary>
            app.UseHttpsRedirection();


            /// <summary>
            /// Enables response caching for better performance by storing responses for reuse.
            /// </summary>
            app.UseResponseCaching();

            /// <summary>
            /// Compresses the response to improve performance by reducing payload size.
            /// </summary>
            app.UseResponseCompression();

            /// <summary>
            /// Serves static files like CSS, JavaScript, or images from the wwwroot folder.
            /// </summary>
            app.UseStaticFiles();

            /// <summary>
            /// Configures the application to comply with the EU General Data Protection Regulation (GDPR).
            /// </summary>
            app.UseCookiePolicy();

            /// <summary>
            /// Enables Swagger UI and API documentation.
            /// </summary>
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewareDemo API");
                c.RoutePrefix = string.Empty; // Serve Swagger UI at the root
            });

            /// <summary>
            /// Adds middleware to route requests to appropriate controllers or endpoints.
            /// </summary>
            app.UseRouting();

            /// <summary>
            /// Adds custom rate limiting middleware for API request throttling.
            /// </summary>
            app.UseRateLimiter();

            /// <summary>
            /// Adds middleware for Cross-Origin Resource Sharing (CORS) policies.
            /// </summary>
            app.UseCors();

            /// <summary>
            /// Adds middleware for authentication to secure resources.
            /// </summary>
            app.UseAuthentication();

            /// <summary>
            /// Authorizes users to access secure endpoints based on their permissions.
            /// </summary>
            app.UseAuthorization();

            /// <summary>
            /// Enables session management in the application.
            /// </summary>
            app.UseSession();


            // Custom middleware

            /// <summary>
            /// Integrates custom middleware into the pipeline.
            /// </summary>
            app.UseMiddleware<CustomMiddleware>();

            /// <summary>
            /// Defines endpoints for controllers and other routable components.
            /// </summary>
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Use(), next() method use into middleware
            // short-circuiting into middleware pipeline
            // middleware chaining

            /// <summary>
            /// Middleware example to demonstrate the use of Use(), next(), and short-circuiting.
            /// </summary>
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 1 Before next().\n");

                // Call the next middleware in the pipeline
                await next(context);

                await context.Response.WriteAsync("Middleware 1 After next().\n");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 2 Before checking condition.\n");

                // Example condition to short-circuit the pipeline
                if (context.Request.Path == "/short-circuit")
                {
                    await context.Response.WriteAsync("Middleware 2 - Short circuiting the pipeline here!\n");
                    return; // Stop further middleware execution
                }

                // Call the next middleware
                await next(context);

                await context.Response.WriteAsync("Middleware 2 After next().\n");
            });

            // Map() 

            /// <summary>
            /// Middleware example to demonstrate route mapping using Map().
            /// </summary>
            app.Map("/map1", HandleMapUsingFunction);
            app.Map("/map2", appMap =>
            {
                appMap.Run(async context =>
                {
                    await context.Response.WriteAsync("Hello from map().\n");
                });
            });

            /// <summary>
            /// Terminal middleware that ends the pipeline with a final response.
            /// </summary>
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Final Middleware.\n");
            });

            /// <summary>
            /// Enables directory browsing to view and access files in a directory.
            /// </summary>
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
                RequestPath = "/image"
            });
        }

        /// <summary>
        /// Custom route handling using Map() to define specific routes and middleware.
        /// </summary>
        private void HandleMapUsingFunction(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Custom middleware method accessed using map().\n");

                // Call the next middleware in the pipeline
                await next(context);
            });
        }
    }
}
