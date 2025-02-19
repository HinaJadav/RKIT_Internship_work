using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ControllerInitializationDemo.Infrastructure
{
    public class CustomControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext context)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();

            // You can add logging or additional logic here
            Console.WriteLine($"Initializing Controller: {controllerType.Name}");

            return ActivatorUtilities.CreateInstance(serviceProvider, controllerType);
        }

        public void Release(ControllerContext context, object controller)
        {
            // Handle cleanup if necessary
        }
    }
}
