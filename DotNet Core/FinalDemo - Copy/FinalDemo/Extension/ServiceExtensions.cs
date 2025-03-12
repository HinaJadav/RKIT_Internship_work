using FinalDemo.BL.Interfaces;
using FinalDemo.BL.Services;
using FinalDemo.DB;
using FinalDemo.Filter;
using Microsoft.Extensions.DependencyInjection;
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

            // Register IDbConnectionFactory as a Singleton (factory remains singleton)
            services.AddSingleton<IDbConnectionFactory>(dbFactory);

            // Register IDbConnection as Scoped (creates a new connection per request)
            services.AddScoped<IDbConnection>(sp =>
                sp.GetRequiredService<IDbConnectionFactory>().OpenDbConnection());

            // Register custom filters for authentication and exception handling
            services.AddScoped<CustomAuthenticationFilter>();
            services.AddScoped<CustomExceptionFilter>();

            // Register application services with dependency injection
            services.AddScoped<IUserService, UserService>();
        }
    }
}
