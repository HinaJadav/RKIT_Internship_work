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
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            

            // Dependency injection
            services.AddScoped<ITaskService, TaskService>();
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