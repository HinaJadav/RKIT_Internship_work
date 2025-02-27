using FinalDemo.BL.Interfaces;
using FinalDemo.Filter;
using FinalDemo.Models;
using FinalDemo.Models.DTOs;
using FinalDemo.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    [ApiController]
    [Route("api/user")]
    [ServiceFilter(typeof(CustomExceptionFilter))] // Global error handling for this controller
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Handles user login.
        /// Authenticates a user and generates a response based on the login attempt.
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] DTOLogin loginDto)
        {
            try
            {
                _logger.LogInformation("Login attempt for user: {Username}", loginDto.U01102);
                Response response = _userService.Login(loginDto);

                if (response.IsError)
                {
                    _logger.LogWarning("Login failed for user {Username}: {Message}", loginDto.U01102, response.Message);
                    return Unauthorized(new { Message = response.Message });
                }

                _logger.LogInformation("User {Username} logged in successfully.", loginDto.U01102);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for user {Username}", loginDto.U01102);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        /// <summary>
        /// Handles user registration.
        /// Registers a new user and validates the provided details before saving the user.
        /// </summary>
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] DTOYMU01 signUpDto)
        {
            try
            {
                _logger.LogInformation("New user signup attempt: {Username}", signUpDto.U01102);

                _userService.PreSaveUser(signUpDto, OperationType.A);

                Response validationResponse = _userService.Validation();
                if (validationResponse.IsError)
                {
                    return BadRequest(new { Message = validationResponse.Message });
                }

                Response response = _userService.Save();
                if (response.IsError)
                {
                    return BadRequest(new { Message = response.Message });
                }

                _logger.LogInformation("User {Username} signed up successfully.", signUpDto.U01102);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Signup failed for user {Username}", signUpDto.U01102);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves user details by user ID.
        /// Fetches and returns the details of the user with the provided ID.
        /// </summary>
        [HttpGet("{userId}")]
        [ServiceFilter(typeof(CustomAuthenticationFilter))]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                _logger.LogInformation("Fetching details for user ID: {UserId}", userId);
                DTOYMU01 user = _userService.GetById(userId);

                if (user == null)
                {
                    _logger.LogWarning("User not found for ID {UserId}", userId);
                    return NotFound(new { Message = "User not found." });
                }

                _logger.LogInformation("Retrieved details for user ID: {UserId}", userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user details for ID {UserId}", userId);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        /// <summary>
        /// Updates an existing user's details.
        /// Validates and updates the details of the user with the provided ID.
        /// </summary>
        [HttpPut("update/{userId}")]
        [ServiceFilter(typeof(CustomAuthenticationFilter))]
        public IActionResult UpdateUser(int userId, [FromBody] DTOYMU01 updateDto)
        {
            try
            {
                _logger.LogInformation("Updating user ID: {UserId}", userId);

                // Validate user existence
                DTOYMU01 existingUser = _userService.GetById(userId);
                if (existingUser == null)
                {
                    _logger.LogWarning("User not found for ID {UserId}", userId);
                    return NotFound(new { Message = "User not found." });
                }

                // Update user details
                _userService.PreSaveUser(updateDto, OperationType.E);

                Response validationResponse = _userService.Validation();
                if (validationResponse.IsError)
                {
                    return BadRequest(new { Message = validationResponse.Message });
                }

                Response response = _userService.Save();
                if (response.IsError)
                {
                    return BadRequest(new { Message = response.Message });
                }

                _logger.LogInformation("User ID {UserId} updated successfully.", userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user ID {UserId}", userId);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// Only authorized users can delete other users' details.
        /// </summary>
        [HttpDelete("{userId}")]
        [ServiceFilter(typeof(CustomAuthenticationFilter))]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete user ID: {UserId}", userId);

                _userService.PreDeleteUser(userId, OperationType.D);

                Response validationResponse = _userService.Validation();
                if (validationResponse.IsError)
                {
                    return BadRequest(new { Message = validationResponse.Message });
                }

                Response response = _userService.Delete();
                if (response.IsError)
                {
                    return BadRequest(new { Message = response.Message });
                }

                _logger.LogInformation("User ID {UserId} deleted successfully.", userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user ID {UserId}", userId);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
