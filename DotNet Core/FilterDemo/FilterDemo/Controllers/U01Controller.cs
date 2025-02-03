using FilterDemo.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FilterDemo.Models;
using FilterDemo.Filters;

namespace FilterDemo.Controllers
{
    [ApiController]
    [Route("u01api")]
    public class U01Controller : ControllerBase
    {
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

        [HttpGet("secure-data")]
        [JwtAuthorization]  // Custom JWT Authorization Filter
        [CustomAuthorizationFilter] // Requires Admin role
        [ServiceFilter(typeof(CustomResourceFilter))] // Custom Resource Filter
        [ServiceFilter(typeof(CustomActionFilter))] // Custom Action Filter
        public IActionResult SecureEndpoint()
        {
            return Ok("This is secured data for Admins.");
        }
    }
}
