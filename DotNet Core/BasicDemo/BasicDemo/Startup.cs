namespace BasicDemo
{
    /// <summary>
    /// This is main configure class of application for configure services or middleware
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Registers services required by the application into the Dependency Injection(DI) container.
        /// After this we can use all these services throughout the application.
        /// Itwill be use for configure services like controllers, database connections or authentication.
        /// </summary>
        /// <param name="services"></param>
        public void configureServices(IServiceCollection services)
        {
            // Adds support for controllers in the application
            // It allows the app to handle incoming HTTP requests via controller actions
            services.AddControllers();

            // Register swagger service
            services.AddSwaggerGen();

        }

        /// <summary>
        /// Sets up the middleware pipeline to handle HTTP requests
        /// Middleware pipeline determines how HTTP requests are handle and how responses are sent back to the client.
        /// </summary>
        /// <param name="app">Used to configure the middleware pipeline</param>
        /// <param name="env">Provides information about the hosting environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            // Checks weather the application is running in the Development environment(like development, staging, production)
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            // Enable static file access from the wwwroot folder
            app.UseStaticFiles();

            // Enable swagger middleware
            // Generate the OpenAI documentation in JSON format
            app.UseSwagger();

            // Enable swagger UI
            // Displays the openAI documentation using a browser-based UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basic demo API");
                c.RoutePrefix = string.Empty;
            });

            

            // Add routing middleware to the pipeline.
            // It matches incoming requests to the appropriate route and prepares it for endpoint execution
            app.UseRouting();

            // Add endpoint middleware which execute the final logic for the matched route.
            app.UseEndpoints(endpoints =>
            {
                // Maps attribute-routed controllers to the routing system.
                endpoints.MapControllers();
            });
        }
    }
}
