using Microsoft.Web.Http;
using Microsoft.Web.Http.Versioning;
using System.Web.Http;
using System.Web.Http.Cors;


namespace StudentManagementSystem
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable API versioning
            config.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new QueryStringApiVersionReader("version");
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            // Enable global CORS
            var cors = new EnableCorsAttribute("*", "*", "GET,POST,PUT,DELETE");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            // Optional: Default route as a fallback
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


        }

    }
}
