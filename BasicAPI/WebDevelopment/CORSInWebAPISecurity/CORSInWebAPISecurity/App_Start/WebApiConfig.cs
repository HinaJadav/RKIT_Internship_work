using System.Web.Http;
using System.Web.Http.Cors;

namespace CORSInWebAPISecurity
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable CORS globally
            var cors = new EnableCorsAttribute(
                origins: "https://localhost:44324", // Allow specific origins
                headers: "*", // Allow all headers
                methods: "GET, POST"  // Allow only GET and POST type of methods
            );
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
