using FilterDemo.Filters;
using Microsoft.OpenApi.Models;

namespace FilterDemo
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
            // Register custom filters as services for dependency injection
            services.AddScoped<CustomAuthorizationFilter>();
            services.AddScoped<CustomResourceFilter>();
            services.AddScoped<CustomActionFilter>();
            services.AddScoped<CustomExceptionFilter>();
            services.AddScoped<CustomResultFilter>();

            // Add MVC services and configure filters globally if needed
            services.AddControllers(options =>
            {
                // Apply filters globally
                options.Filters.AddService<CustomAuthorizationFilter>(); // JWT Authorization
                options.Filters.AddService<CustomResourceFilter>(); // Resource Execution
                options.Filters.AddService<CustomActionFilter>(); // Action Execution
                options.Filters.AddService<CustomExceptionFilter>(); // Global Exception Handling
                options.Filters.AddService<CustomResultFilter>(); // Result Execution
            });

            // Enable Swagger with JWT authentication support
            ConfigureSwagger(services);
        }

        /// <summary>
        /// Configures Swagger for API documentation with JWT authentication support.
        /// </summary>
        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilterDemo API", Version = "v1" });

                // Define the JWT Bearer authentication scheme
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and your JWT token. Example: Bearer your_token_here"
                });

                // Apply the JWT security requirement to all API endpoints
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Configures the middleware pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Enables detailed error pages in development
            }

            // Enable global exception handling middleware (If you create a middleware-based exception handler)
            app.UseMiddleware<CustomExceptionFilter>();

            // Enable Swagger for API documentation
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FilterDemo API V1");
                c.RoutePrefix = "swagger"; // Swagger UI will be available at "/swagger"
            });

            // Enable routing
            app.UseRouting();

            // Enable authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure endpoint routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
