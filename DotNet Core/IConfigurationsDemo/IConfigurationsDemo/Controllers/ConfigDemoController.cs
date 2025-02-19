using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace IConfigurationsDemo.Controllers
{
    [ApiController]
    [Route("api/config")]
    public class ConfigDemoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigDemoController"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public ConfigDemoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieves a configuration value by key.
        /// </summary>
        /// <param name="key">The configuration key (e.g., "AppSettings:AppName").</param>
        /// <returns>The value associated with the provided key.</returns>
        /// <example>
        /// Request: GET /api/config/getValue?key=AppSettings:AppName  
        /// Response:
        /// {
        ///   "Key": "AppSettings:AppName",
        ///   "Value": "Configuration Demo"
        /// }
        /// </example>
        [HttpGet("getValue")]
        public IActionResult GetConfigValue([FromQuery] string key)
        {
            return Ok(new { Key = key, Value = _configuration[key] });
        }

        /// <summary>
        /// Retrieves a configuration section and its key-value pairs.
        /// </summary>
        /// <param name="sectionKey">The key of the configuration section (e.g., "AppSettings").</param>
        /// <returns>A dictionary containing the key-value pairs in the specified section.</returns>
        /// <example>
        /// Request: GET /api/config/getSection?sectionKey=AppSettings  
        /// Response:
        /// {
        ///   "sectionKey": "AppSettings",
        ///   "values": {
        ///     "AppName": "Configuration Demo",
        ///     "Version": "1.0.0"
        ///   }
        /// }
        /// </example>
        [HttpGet("getSection")]
        public IActionResult GetConfigSection([FromQuery] string sectionKey)
        {
            var section = _configuration.GetSection(sectionKey);
            return Ok(new
            {
                SectionKey = sectionKey,
                Values = section.GetChildren().ToDictionary(x => x.Key, x => x.Value)
            });
        }

        /// <summary>
        /// Retrieves all child keys and values under a given section.
        /// </summary>
        /// <param name="sectionKey">The section key to get children from (e.g., "AppSettings").</param>
        /// <returns>A list of child key-value pairs.</returns>
        /// <example>
        /// Request: GET /api/config/getChildren?sectionKey=AppSettings  
        /// Response:
        /// [
        ///   {
        ///     "key": "AppName",
        ///     "value": "Configuration Demo"
        ///   },
        ///   {
        ///     "key": "Version",
        ///     "value": "1.0.0"
        ///   }
        /// ]
        /// </example>
        [HttpGet("getChildren")]
        public IActionResult GetChildren([FromQuery] string sectionKey)
        {
            var children = _configuration.GetSection(sectionKey).GetChildren();
            return Ok(children.Select(x => new { x.Key, x.Value }));
        }

        /// <summary>
        /// Returns a reload token that listens for configuration changes.
        /// </summary>
        /// <returns>A message indicating that the system is monitoring configuration changes.</returns>
        /// <example>
        /// Request: GET /api/config/reloadToken  
        /// Response:
        /// {
        ///   "message": "Listening for configuration changes..."
        /// }
        /// </example>
        [HttpGet("reloadToken")]
        public IActionResult GetReloadToken()
        {
            IChangeToken token = _configuration.GetReloadToken();
            return Ok(new { Message = "Listening for configuration changes..." });
        }
    }
}


// this[string key] : Used in GetConfigValue to fetch config values.
// GetSection(string key) : Used in GetConfigSection to retrieve subsections.
// GetChildren() : Used in GetChildren to list all child configurations.

// GetReloadToken() : Used in GetReloadToken to detect changes in config files.
/*Use Cases of GetReloadToken():
Feature Flags: Automatically enable/disable features when appsettings.json is updated.
Connection Strings: Reload database connections dynamically.
Logging Levels: Adjust log severity in real-time without restarting the service.
API Rate Limits: Update API rate limit settings dynamically.*/

