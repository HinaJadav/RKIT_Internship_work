using FilterDemo.BL.Service;
using FilterDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilterDemo.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(IConfiguration configuration)
        {
            var key = config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key cannot be null.");
            _authService = new AuthService(key);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] YMU01 loginRequest)
        {
            var user = _authService.ValidateUser(loginRequest.U01F02, loginRequest.U01F03);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
