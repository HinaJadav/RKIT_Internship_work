using FinalDemo.BL.Interfaces;
using FinalDemo.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using FinalDemo.Filter;

namespace FinalDemo.Controllers
{
    [ApiController]
    [Route("api/user")]
    [ServiceFilter(typeof(CustomExceptionFilter))] // Global error handling for this controller
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Handles user login requests.
        /// </summary>
        [HttpPost("login")]
        [ServiceFilter(typeof(CustomAuthenticationFilter))] // Custom authentication applied
        public IActionResult Login([FromBody] DTOLogin loginDto)
        {
            try
            {
                _logger.LogInformation("Login attempt for user: {Username}", loginDto.U01102);
                var user = _userService.Login(loginDto);
                _logger.LogInformation("User {Username} logged in successfully.", loginDto.U01102);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Login failed for user {Username}: {Message}", loginDto.U01102, ex.Message);
                return Unauthorized(new { Message = "Invalid username or password." });
            }
        }

        /// <summary>
        /// Handles new user registrations.
        /// </summary>
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] DTOSignUp signUpDto)
        {
            try
            {
                _logger.LogInformation("New user signup attempt: {Username}", signUpDto.U01102);
                var newUser = _userService.SignUp(signUpDto);
                _logger.LogInformation("User {Username} signed up successfully.", signUpDto.U01102);
                return CreatedAtAction(nameof(GetUserById), new { userId = newUser.U01101 }, newUser);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Signup failed for user {Username}: {Message}", signUpDto.U01102, ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves user details by ID.
        /// </summary>
        [HttpGet("{userId}")]
        [ServiceFilter(typeof(CustomAuthenticationFilter))] // Ensure user is authenticated
        public IActionResult GetUserById(int userId)
        {
            try
            {
                _logger.LogInformation("Fetching details for user ID: {UserId}", userId);
                var user = _userService.GetUserById(userId);
                _logger.LogInformation("Retrieved details for user ID: {UserId}", userId);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("User not found for ID {UserId}: {Message}", userId, ex.Message);
                return NotFound(new { Message = "User not found." });
            }
        }
    }
}
