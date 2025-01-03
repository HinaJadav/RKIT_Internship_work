using System.Web.Http;

namespace APIVersioning_In_WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable attribute routing
            config.MapHttpAttributeRoutes();

            // Optionally, you can define default routes like this (though attribute routing is usually enough)
            // This is just an example in case you need to use conventional routing alongside attribute routing.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
