using MiddlewareDemo;

var builder = WebApplication.CreateBuilder(args);

// Initialize Startup
var startup = new Startup(builder.Configuration);

// Configure services
startup.ConfigureServices(builder.Services);

// Run the application
var app = builder.Build();

// Configure middleware
startup.Configure(app, app.Environment);

app.Run();
