using System;

namespace BasicDemo
{
    class Program
    {
        // application entry point
        // it will invoked when application start and initializes the host for the application

        static void Main(string[] args)
        {
            // build and run host 
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method for set up host builder
        /// It creates default host for the application
        /// It will includes configuration, logging etc for hosting application.
        /// </summary>
        /// <param name="args">it take values as argument parameters which we give as input into CLI</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            
            return Host.CreateDefaultBuilder 
            (args).ConfigureWebHostDefaults(webHost =>
            {
                webHost.UseStartup<Startup>();
            });
        }
    }
}

// ConfigureWebHostDefaults: Configure the web host with default settings(this configuration from appsettings.json)

// startup class: Use to configure the application's services and middleware. Also it defines that how the app will respond to HTTp requests.