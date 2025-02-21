using DIDemo.BL;

namespace DIDemo
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
        /// 
        /// 4. Extension Methods for Registration
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {


            // Dependency injection

            // 3. Understanding Service Lifetime

            // ITaskService is registered as Scoped because it manages data that could change per request.
            services.AddScoped<ITaskService, TaskService>();

            // ILoggerService is registered as Singleton because logging services are typically stateless and shared across the application.
            services.AddSingleton<ILoggerService, ConsoleLoggerService>();



            services.AddControllers();

            // Add Swagger services
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the middleware pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // Enable Swagger middleware
            app.UseSwagger();

            // Enable Swagger UI middleware
            app.UseSwaggerUI();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}