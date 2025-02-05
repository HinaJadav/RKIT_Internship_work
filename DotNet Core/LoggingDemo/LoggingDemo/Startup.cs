using Serilog;

namespace LoggingDemo
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

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        /// <summary>
        /// Configures services required by the application and registers them for Dependency Injection.
        /// </summary>
        /// <param name="services">Service collection to add dependencies.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();  // Remove default providers

                // check into Console Window 
                loggingBuilder.AddConsole();     // Enable Console logging
                // check into Debug window
                loggingBuilder.AddDebug();       // Enable Debug logging


                // Add logging with Serilog - Third party logging
                loggingBuilder.AddSerilog();
            });

            // Add support for controllers in the application.
            services.AddControllers();
        }

        /// <summary>
        /// Configures the middleware pipeline to handle HTTP requests and exceptions.
        /// </summary>
        /// <param name="app">Provides mechanisms to configure the application's request pipeline.</param>
        /// <param name="env">Provides information about the application's hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enables routing for request handling.
            app.UseRouting();

            // Configures application endpoints.
            app.UseEndpoints(endpoints =>
            {
                // Maps controller routes.
                endpoints.MapControllers();
            });

            // Log information after the middleware pipeline is set up.
            logger.LogInformation("Application has started successfully.");
        }
    }
}