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

            
            app.UseRouting();

            // Conventional routing : 

            app.UseEndpoints(endpoints =>
            {
                // Default route
                // http://localhost:25800/
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Custom route for 'About' section
                // http://localhost:25800/about-us
                endpoints.MapControllerRoute(
                    name: "aboutRoute",
                    pattern: "about-us",
                    defaults: new { controller = "Home", action = "About" });
            });
        }
    }
}
