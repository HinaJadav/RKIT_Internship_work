using Microsoft.AspNetCore.Mvc;

namespace LoggingDemo.Controllers
{
    /// <summary>
    /// HomeController is responsible for handling HTTP requests related to home actions.
    /// It demonstrates how to use logging in ASP.NET Core Web API.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// </summary>
        /// <param name="logger">An instance of ILogger to log messages related to HomeController actions.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles the HTTP GET request to fetch a response indicating that logging is working.
        /// This method demonstrates different logging levels.
        /// </summary>
        /// <returns>An HTTP response indicating logging functionality is working.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            // Log an informational message
            _logger.LogInformation("Executing Get method in HomeController.");

            // Log a warning message
            _logger.LogWarning("This is a warning message.");

            // Log an error message
            _logger.LogError("An error occurred while executing Get method.");

            // Return a success response
            return Ok("Logging is working in ASP.NET Core Web API!");
        }
    }
}
