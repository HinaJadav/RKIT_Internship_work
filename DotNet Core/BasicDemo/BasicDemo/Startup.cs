using Microsoft.Extensions.FileProviders;

namespace BasicDemo
{
    /// <summary>
    /// This is main configure class of application for configure services or middleware
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// IConfiguration is an interface in ASP.NET Core used for managing configuration settings.
        /// It allows accessing key-value pairs from various configuration sources, such as:
        /// - appsettings.json
        /// - Environment Variables
        /// - Command-Line Arguments
        /// </summary>
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Registers services required by the application into the Dependency Injection(DI) container.
        /// After this we can use all these services throughout the application.
        /// It will be use for configure services like controllers, database connections or authentication.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds support for controllers in the application
            // It allows the app to handle incoming HTTP requests via controller actions
            services.AddControllers();

            // Register swagger service
            services.AddSwaggerGen();

        }

        /// <summary>
        /// Sets up the middleware pipeline to handle HTTP requests
        /// Middleware pipeline determines how HTTP requests are handle and how responses are sent back to the client.
        /// </summary>
        /// <param name="app">Used to configure the middleware pipeline</param>
        /// <param name="env">Provides information about the hosting environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Checks weather the application is running in the Development environment(like development, staging, production)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable static file access from the wwwroot folder
            app.UseStaticFiles();

            // Enables serving static files from custom static files folder
            // FileProvider : responsible for handling file system access
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Customwwwroot")),
                RequestPath = ""
            });

            // Enable swagger middleware
            app.UseSwagger();

            // Enable swagger UI
            // Displays the openAI documentation using a browser-based UI
            app.UseSwaggerUI();

            // Add routing middleware to the pipeline.
            // It matches incoming requests to the appropriate route and prepares it for endpoint execution
            app.UseRouting();

            // Add endpoint middleware which execute the final logic for the matched route.
            app.UseEndpoints(endpoints =>
            {
                // Maps attribute-routed controllers to the routing system.
                endpoints.MapControllers();
            });
        }
    }
}
