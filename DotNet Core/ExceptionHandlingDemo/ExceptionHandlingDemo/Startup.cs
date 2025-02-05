using Microsoft.AspNetCore.Diagnostics;

namespace ExceptionHandlingDemo
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration settings.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configures services required by the application and registers them for Dependency Injection.
        /// </summary>
        /// <param name="services">Service collection to add dependencies.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add support for controllers in the application.
            services.AddControllers();
        }

        /// <summary>
        /// Configures the middleware pipeline to handle HTTP requests and exceptions.
        /// </summary>
        /// <param name="app">Provides mechanisms to configure the application's request pipeline.</param>
        /// <param name="env">Provides information about the application's hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Enables detailed exception information in development mode.
                app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions
                {
                    // Limit the number of lines displayed in the stack trace for better readability.
                    SourceCodeLineCount = 3 // Show only 3 lines of code in stack trace.
                });
            }
            else
            {
                // Enables global exception handling middleware in production.
                app.UseExceptionHandler();
            }

            // Enables routing for request handling.
            app.UseRouting();

            // Configures application endpoints.
            app.UseEndpoints(endpoints =>
            {
                // Maps controller routes.
                endpoints.MapControllers();

                // Maps a simple error response route.
                endpoints.MapGet("/error", async context =>
                {
                    await context.Response.WriteAsync("An error occurred. Please try again later.");
                });
            });
        }
    }
}
