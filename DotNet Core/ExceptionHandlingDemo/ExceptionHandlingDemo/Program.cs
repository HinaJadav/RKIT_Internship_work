using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Exception Handling Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Shows detailed exception page in development
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                var errorResponse = new { Message = "An unexpected error occurred." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        });
    });
}

app.UseRouting();

app.MapGet("/nullreference", () => Task.FromException(new NullReferenceException("This is a NullReferenceException")));
app.MapGet("/invalidoperation", () => Task.FromException(new InvalidOperationException("This is an InvalidOperationException")));
app.MapGet("/dividebyzero", () => Task.FromException(new DivideByZeroException("This is a DivideByZeroException")));
app.MapGet("/outofrange", () => Task.FromException(new IndexOutOfRangeException("This is an IndexOutOfRangeException")));
app.MapGet("/format", () => Task.FromException(new FormatException("This is a FormatException")));
app.MapGet("/error", () => Task.FromException(new Exception("This is a general exception")));


app.Run();
