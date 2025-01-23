using System.Web.Http;

namespace APIVersioning
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the Web API configuration and routes.
        /// </summary>
        /// <param name="config">The HttpConfiguration object used to configure Web API.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable attribute routing for controller-based routing
            config.MapHttpAttributeRoutes();

            // Custom API versioning routes

            /// <summary>
            /// Maps the route for version 1 of the API.
            /// </summary>
            config.Routes.MapHttpRoute(
                name: "ApiV1",
                routeTemplate: "api/v1/students/{id}",
                defaults: new { controller = "StudentsV1", id = RouteParameter.Optional }
            );

            /// <summary>
            /// Maps the route for version 2 of the API.
            /// </summary>
            config.Routes.MapHttpRoute(
                name: "ApiV2",
                routeTemplate: "api/v2/students/{id}",
                defaults: new { controller = "StudentsV2", id = RouteParameter.Optional }
            );

            // Default route if versioning is not used explicitly
            /// <summary>
            /// Maps the default route for the API without versioning.
            /// </summary>
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
