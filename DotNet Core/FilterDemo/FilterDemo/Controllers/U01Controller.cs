using FilterDemo.BL;
using FilterDemo.Filters;
using FilterDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilterDemo.Controllers
{
    /// <summary>
    /// API controller for handling authentication and secured data access.
    /// </summary>
    [ApiController]
    [Route("u01api")]
    public class U01Controller : ControllerBase
    {
        /// <summary>
        /// Handles user login and returns a JWT token upon successful authentication.
        /// </summary>
        /// <param name="user">User credentials containing username and password.</param>
        /// <returns>JWT token if authentication is successful; otherwise, an error response.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] YMU01 user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (user.U01F01 == "admin" && user.U01F02 == "password")
            {
                // Generate the token using JwtHelper
                var token = JwtHelper.GenerateToken(user.U01F01);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        /// <summary>
        /// Secure endpoint that requires JWT authentication and admin authorization.
        /// Applies multiple custom filters for additional security and logging.
        /// </summary>
        /// <returns>Secured data accessible only to authenticated Admin users.</returns>
        [HttpGet("secure-data")]
        //[JwtAuthorization]  // Custom JWT Authorization Filter
        //[CustomAuthorizationFilter] // Requires Admin role
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        [ServiceFilter(typeof(CustomResourceFilter))] // Custom Resource Filter
        [ServiceFilter(typeof(CustomActionFilter))] // Custom Action Filter
        [ServiceFilter(typeof(CustomExceptionFilter))] // Catches exceptions from action execution
        [ServiceFilter(typeof(CustomResultFilter))] // Logs result execution (before & after result)
        public IActionResult SecureEndpoint()
        {
            return Ok("This is secured data for Admins.");
        }
    }
}
