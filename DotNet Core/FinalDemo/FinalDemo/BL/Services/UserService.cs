using FinalDemo.Models.DTOs;
using FinalDemo.BL.Interfaces;
using FinalDemo.Extension;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalDemo.Services
{
    /// <summary>
    /// Service for managing user authentication and retrieval.
    /// </summary>
    public class UserService : IUserService
    {
        private static readonly List<YMU01> _users = new();
        private static int _userIdCounter = 1;
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging information.</param>
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user based on login credentials.
        /// </summary>
        /// <param name="loginDto">DTO containing username and password.</param>
        /// <returns>Returns user details if authentication is successful.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if credentials are invalid.</exception>
        public DTOResponse Login(DTOLogin loginDto)
        {
            _logger.LogInformation("Executing Login method.");
            var user = _users.FirstOrDefault(u => u.U01F02 == loginDto.U01102 && u.U01F03 == loginDto.U01103);

            if (user == null)
            {
                _logger.LogWarning("Invalid username or password.");
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return user.ToDto<DTOResponse>();
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="signUpDto">DTO containing user registration details.</param>
        /// <returns>Returns the newly created user details.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the username already exists.</exception>
        public DTOResponse SignUp(DTOSignUp signUpDto)
        {
            _logger.LogInformation("Executing SignUp method.");

            if (_users.Exists(u => u.U01F02 == signUpDto.U01102))
            {
                _logger.LogWarning("Username already exists.");
                throw new InvalidOperationException("Username already exists.");
            }

            var newUser = signUpDto.ToPoco<YMU01>(); // Convert DTO to POCO
            newUser.U01F01 = _userIdCounter++; // Assign unique ID

            _users.Add(newUser);
            _logger.LogInformation("User successfully signed up with ID {UserId}.", newUser.U01F01);

            return newUser.ToDto<DTOResponse>(); // Convert POCO back to DTO before returning
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>Returns user details if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the user is not found.</exception>
        public DTOResponse GetUserById(int userId)
        {
            _logger.LogInformation("Executing GetUserById method.");

            var user = _users.FirstOrDefault(u => u.U01F01 == userId);

            if (user == null)
            {
                _logger.LogError("User with ID {UserId} not found.", userId);
                throw new KeyNotFoundException("User not found.");
            }

            return user.ToDto<DTOResponse>();
        }
    }
}
