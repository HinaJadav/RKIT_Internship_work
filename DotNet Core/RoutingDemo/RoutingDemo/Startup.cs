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

            // Routing : 

            

           

            app.UseEndpoints(endpoints =>
            {
                // 1) Conventional routing :
                // use: Into controller based app with multiple action methods
                // # Enable comments for Conventional routing  


                /*// Default route
                // http://localhost:25800/
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Custom route for 'About' section
                // http://localhost:25800/about-us
                endpoints.MapControllerRoute(
                    name: "aboutRoute",
                    pattern: "about-us",
                    defaults: new { controller = "Home", action = "About" });*/


                // # Disable below line during Conventional routing

                // 2) Attribute routing:
                // use: Need more control over individual routes and routes don't follow a common pattern

                endpoints.MapControllers();


 
                // # route handlers(Minimal API routing) : here use as endpoint methods 
                // we can also use them as middleware 
                // Use: lightweight APIs without controllers, for MicroServices and for Minimal APIs
                // work as Simple endpoints for apis


                // a) Simple GET request
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello, World!");
                });

                // b) Route with parameter
                endpoints.MapGet("/hello/{name}", async context =>
                {
                    var name = context.Request.RouteValues["name"]?.ToString();
                    await context.Response.WriteAsync($"Hello, {name}!");
                });

                // c) Route with query parameters
                endpoints.MapGet("/add", async context =>
                {
                    var query = context.Request.Query;
                    if (query.ContainsKey("a") && query.ContainsKey("b"))
                    {
                        int a = int.Parse(query["a"]);
                        int b = int.Parse(query["b"]);
                        await context.Response.WriteAsync($"Sum: {a + b}");
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Please provide 'a' and 'b' as query parameters.");
                    }
                });

                // d) POST request handling
                endpoints.MapPost("/submit", async context =>
                {
                    using var reader = new StreamReader(context.Request.Body);
                    var content = await reader.ReadToEndAsync();
                    await context.Response.WriteAsync($"Received: {content}");
                });
            });

      
        }
    }
}
// what is difference between filter and middleware 
/*.NET Core Routing

Uses Microsoft.AspNetCore.Routing.
Middleware-based (UseRouting, UseEndpoints).
Attribute routing is primary.
More performant and flexible.
Supports advanced route constraints and endpoint filters.*/