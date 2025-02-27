using FinalDemo;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
logger.Info("Application starting...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add NLog as the logging provider
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Initialize Startup
    var startup = new Startup(builder.Configuration);

    // Configure services
    startup.ConfigureServices(builder.Services);

    var app = builder.Build();

    // Configure middleware
    startup.Configure(app, app.Environment);

    // Run the application
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application stopped due to an unexpected exception.");
    throw;
}
finally
{
    LogManager.Shutdown(); // Ensure logs are flushed before exit
}
