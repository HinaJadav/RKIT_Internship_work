using ControllerInitializationDemo.BL;
using ControllerInitializationDemo.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ControllerInitializationDemo
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

            services.AddScoped<BLIUser, BLUser>();

            // Register Custom Controller Activator
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, CustomControllerActivator>());

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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
