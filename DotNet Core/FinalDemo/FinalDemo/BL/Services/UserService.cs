using FinalDemo.BL.Interfaces;
using FinalDemo.Extension;
using FinalDemo.Models.DTOs;
using ServiceStack.OrmLite;
using ServiceStack.Data;
using System.Data;
using FinalDemo.Models.POCOs;

namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Service for managing user authentication and retrieval with per-user rate limiting.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly ILogger<UserService> _logger;
        private readonly Dictionary<string, (int RequestCount, DateTime LastRequestTime)> _loginAttempts = new();
        private const int MAX_LOGIN_ATTEMPTS = 5;
        private static readonly TimeSpan LOGIN_ATTEMPT_RESET_TIME = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService(IDbConnectionFactory dbFactory, ILogger<UserService> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user based on login credentials with rate limiting.
        /// </summary>
        public DTOResponse Login(DTOLogin loginDto)
        {
            _logger.LogInformation("Executing Login method for {Username}.", loginDto.U01102);

            // Apply rate limiting on login attempts based on username
            if (_loginAttempts.TryGetValue(loginDto.U01102, out var attempts))
            {
                if (attempts.RequestCount >= MAX_LOGIN_ATTEMPTS && DateTime.UtcNow - attempts.LastRequestTime < LOGIN_ATTEMPT_RESET_TIME)
                {
                    throw new InvalidOperationException("Too many login attempts. Please try again later.");
                }
            }

            using var db = _dbFactory.OpenDbConnection();
            var user = db.Single<YMU01>(u => u.U01F02 == loginDto.U01102 && u.U01F03 == loginDto.U01103);

            if (user == null)
            {
                _logger.LogWarning("Invalid username or password for {Username}.", loginDto.U01102);

                // Increase failed attempt count
                if (_loginAttempts.ContainsKey(loginDto.U01102))
                {
                    _loginAttempts[loginDto.U01102] = (attempts.RequestCount + 1, DateTime.UtcNow);
                }
                else
                {
                    _loginAttempts[loginDto.U01102] = (1, DateTime.UtcNow);
                }

                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            // Reset login attempts on successful login
            _loginAttempts.Remove(loginDto.U01102);

            return user.ToDto<DTOResponse>();
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        public DTOResponse SignUp(DTOSignUp signUpDto)
        {
            _logger.LogInformation("Executing SignUp method.");

            using var db = _dbFactory.OpenDbConnection();

            if (db.Exists<YMU01>(u => u.U01F02 == signUpDto.U01102))
            {
                _logger.LogWarning("Username already exists: {Username}.", signUpDto.U01102);
                throw new InvalidOperationException("Username already exists.");
            }

            YMU01 newUser = signUpDto.ToPoco<YMU01>();

            // Insert new user and get the generated ID
            newUser.U01F01 = (int)db.Insert(newUser, true);


            _logger.LogInformation("User successfully signed up with ID {UserId}.", newUser.U01F01);

            return newUser.ToDto<DTOResponse>();
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        public DTOResponse GetUserById(int userId)
        {
            _logger.LogInformation("Executing GetUserById method for {UserId}.", userId);

            using var db = _dbFactory.OpenDbConnection();
            YMU01 user = db.SingleById<YMU01>(userId);

            if (user == null)
            {
                _logger.LogError("User with ID {UserId} not found.", userId);
                throw new KeyNotFoundException("User not found.");
            }

            return user.ToDto<DTOResponse>();
        }
    }
}
