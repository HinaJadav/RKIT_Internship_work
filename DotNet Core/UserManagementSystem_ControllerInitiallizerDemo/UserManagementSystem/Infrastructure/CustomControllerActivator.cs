using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ControllerInitializationDemo.Infrastructure
{
    /// <summary>
    /// Custom controller activator for manually controlling the initialization of controllers.
    /// </summary>
    public class CustomControllerActivator : IControllerActivator
    {
        /// <summary>
        /// Creates a controller instance using the dependency injection container.
        /// </summary>
        /// <param name="context">The controller context that provides request-specific information.</param>
        /// <returns>An instance of the requested controller.</returns>
        public object Create(ControllerContext context)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();

            // Log controller initialization
            Console.WriteLine($"Initializing Controller: {controllerType.Name}");

            return ActivatorUtilities.CreateInstance(serviceProvider, controllerType);
        }

        /// <summary>
        /// Releases the controller instance when it is no longer needed.
        /// </summary>
        /// <param name="context">The controller context.</param>
        /// <param name="controller">The controller instance to be released.</param>
        public void Release(ControllerContext context, object controller)
        {
            // Handle cleanup if necessary (e.g., disposing resources)
        }
    }
}
