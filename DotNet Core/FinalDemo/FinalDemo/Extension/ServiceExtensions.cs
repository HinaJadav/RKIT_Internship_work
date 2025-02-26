using FinalDemo.BL.Interfaces;
using FinalDemo.BL.Services;
using FinalDemo.Filter;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace FinalDemo.Extension
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register OrmLite DB Factory
            var dbFactory = new OrmLiteConnectionFactory(
                configuration.GetConnectionString("MyDbConnection"),
                MySqlDialect.Provider
            );

            services.AddSingleton<IDbConnectionFactory>(dbFactory);
            services.AddScoped<IDbConnection>(sp => sp.GetRequiredService<IDbConnectionFactory>().OpenDbConnection());

            // Register Filters
            services.AddSingleton<CustomAuthenticationFilter>();
            services.AddSingleton<CustomExceptionFilter>();

            // Register Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBugService, BugService>();
        }
    }
}
