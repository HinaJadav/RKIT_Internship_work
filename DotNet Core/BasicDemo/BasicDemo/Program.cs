// program.cs:
// It contains entry point for the application

using BasicDemo;

// builder: instance of web application builder
// args: used to pass CL-args(inputs) into project
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