using FinalDemo.BL.Interfaces;
using FinalDemo.BL.Services;
using FinalDemo.Filter;
using FinalDemo.Middleware;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace FinalDemo.Extension
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds application services to the DI container.
        /// Registers database connection, filters, and services.
        /// </summary>
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register OrmLite DB Factory for database connections
            var dbFactory = new OrmLiteConnectionFactory(
                configuration.GetConnectionString("MyDbConnection"),
                MySqlDialect.Provider
            );

            // Register IDbConnectionFactory and IDbConnection for DB access
            services.AddSingleton<IDbConnectionFactory>(dbFactory);
            services.AddScoped<IDbConnection>(sp => sp.GetRequiredService<IDbConnectionFactory>().OpenDbConnection());

            // Register custom filters for authentication and exception handling
            services.AddScoped<CustomAuthenticationFilter>();
            services.AddScoped<CustomExceptionFilter>();

            // Register application services with dependency injection
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBugService, BugService>();

            
        }
    }
}
