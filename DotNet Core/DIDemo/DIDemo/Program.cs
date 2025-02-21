using DIDemo;

var builder = WebApplication.CreateBuilder(args);

// Initialize Startup
var startup = new Startup(builder.Configuration);

// Configure services
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure middleware
startup.Configure(app, app.Environment);

// Run the application
app.Run();

// https://medium.com/@ravipatel.it/dependency-injection-and-services-in-asp-net-core-a-comprehensive-guide-dd69858c1eab