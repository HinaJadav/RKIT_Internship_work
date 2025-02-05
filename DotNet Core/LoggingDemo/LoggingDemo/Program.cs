using LoggingDemo;

var builder = WebApplication.CreateBuilder(args);

// Initialize and configure services via Startup
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure middleware via Startup
var logger = app.Services.GetRequiredService<ILogger<Startup>>(); // Resolve logger
startup.Configure(app, app.Environment, logger);  // Call Configure from Startup

// Run the application
app.Run();
