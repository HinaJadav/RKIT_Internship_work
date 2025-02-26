using FinalDemo.DB;
using FinalDemo.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;
using System.Data;
using System.Text;

namespace FinalDemo
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add database connection
            services.AddSingleton<IDbConnection>(sp => DBConnection.OpenConnection(_configuration));


            // Add logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            // Add controllers
            services.AddControllers();

            // Register application services (includes OrmLite DI)
            services.AddApplicationServices(_configuration);

            // Configure CORS (Allow All for Development)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            // Configure JWT Authentication
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Add Authorization
            services.AddAuthorization();

            // Register Swagger for API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BugTracking API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter 'Bearer' [space] and then your token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Handle exceptions differently for Development and Production
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Enable CORS
            app.UseCors("AllowAllOrigins");

            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BugTracking API v1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/error", async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An unexpected error occurred.");
                });
            });

            // Initialize Database
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IDbConnection>();
                DBConnection.OpenConnection(_configuration);
            }
        }
    }
}
// CUSTOM MIDDLEWARE
