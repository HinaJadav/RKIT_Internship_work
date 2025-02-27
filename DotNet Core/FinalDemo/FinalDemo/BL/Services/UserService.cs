using FinalDemo.BL.Interfaces;
using FinalDemo.Extension;
using FinalDemo.Models;
using FinalDemo.Models.DTOs;
using FinalDemo.Models.Enums;
using FinalDemo.Models.POCOs;
using Microsoft.IdentityModel.Tokens;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinalDemo.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly ILogger<UserService> _logger;
        private YMU01 _userObj;
        private int _userId;
        private Response _response;
        public OperationType Type { get; set; }
        private readonly IConfiguration _configuration;
        private static readonly Dictionary<string, (int RequestCount, DateTime LastRequestTime)> _loginAttempts = new();
        private const int MAX_LOGIN_ATTEMPTS = 5;
        private static readonly TimeSpan LOGIN_ATTEMPT_RESET_TIME = TimeSpan.FromMinutes(1);

        public UserService(IConfiguration configuration, IDbConnectionFactory dbFactory, ILogger<UserService> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
            _configuration = configuration;
            _response = new Response();
        }

        /// <summary>
        /// Prepares user object for saving or updating based on the provided data.
        /// </summary>
        public void PreSaveUser(DTOYMU01 dtoUser, OperationType operationType, int? userId = null)
        {
            Type = operationType;

            if (userId.HasValue)
            {
                _userId = userId.Value; // Set user ID for validation
                using IDbConnection db = _dbFactory.Open();
                YMU01 existingUser = db.SingleById<YMU01>(_userId);

                if (existingUser == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }

                // Update existing user fields selectively (only update non-null properties)
                _userObj = existingUser;
                _userObj.U01F02 = dtoUser.U01102 ?? _userObj.U01F02;
                _userObj.U01F03 = string.IsNullOrEmpty(dtoUser.U01103) ? _userObj.U01F03 : HashPassword(dtoUser.U01103);
                _userObj.U01F04 = dtoUser.U01104 ?? _userObj.U01F04;
            }
            else
            {
                // New user creation
                _userObj = dtoUser.ToPoco<YMU01>();
            }
        }

        /// <summary>
        /// Prepares the user object for login by hashing the password.
        /// </summary>
        public void PreLoginUser(DTOLogin dtoUser)
        {
            dtoUser.U01103 = HashPassword(dtoUser.U01103);
            _userObj = dtoUser.ToPoco<YMU01>();
        }

        /// <summary>
        /// Validates whether the user exists in the database.
        /// </summary>
        private bool IsUserExist(int userId)
        {
            using IDbConnection db = _dbFactory.Open();
            return db.Exists<YMU01>(userId);
        }

        /// <summary>
        /// Prepares the user for deletion by setting the user ID and operation type.
        /// </summary>
        public void PreDeleteUser(int userId, OperationType operationType)
        {
            Type = operationType;
            _userId = userId;
        }

        /// <summary>
        /// Validates the user data based on the operation type (Add, Edit, Delete).
        /// </summary>
        public Response Validation()
        {
            if (Type == OperationType.E || Type == OperationType.D)
            {
                if (_userId <= 0 || !IsUserExist(_userId))
                {
                    _response.IsError = true;
                    _response.Message = "Invalid or non-existing user.";
                }
            }
            return _response;
        }

        /// <summary>
        /// Saves the user data based on the operation type (Add, Edit).
        /// </summary>
        public Response Save()
        {
            try
            {
                using IDbConnection db = _dbFactory.Open();
                if (Type == OperationType.A)
                {
                    db.Save(_userObj);
                    _response.Message = "User registered successfully.";
                }
                else if (Type == OperationType.E)
                {
                    if (!IsUserExist(_userId))
                    {
                        _response.IsError = true;
                        _response.Message = "User not found.";
                        return _response;
                    }

                    db.Update(_userObj);
                    _response.Message = "User details updated successfully.";
                }
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
            }
            return _response;
        }

        /// <summary>
        /// Hashes the user password using SHA256.
        /// </summary>
        private string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        /// <summary>
        /// Deletes the user based on user ID.
        /// </summary>
        public Response Delete()
        {
            try
            {
                using IDbConnection db = _dbFactory.Open();
                db.DeleteById<YMU01>(_userId);
                _response.Message = "User deleted successfully.";
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Retrieves a user by its ID.
        /// </summary>
        public DTOYMU01 GetById(int id)
        {
            using IDbConnection db = _dbFactory.Open();
            YMU01 user = db.SingleById<YMU01>(id);
            return user.ToDto<DTOYMU01>();
        }

        /// <summary>
        /// Handles user login by validating credentials and generating JWT token.
        /// </summary>
        public Response Login(DTOLogin loginDto)
        {
            if (_loginAttempts.TryGetValue(loginDto.U01102, out var attempts))
            {
                if (attempts.RequestCount >= MAX_LOGIN_ATTEMPTS && DateTime.UtcNow - attempts.LastRequestTime < LOGIN_ATTEMPT_RESET_TIME)
                {
                    _response.IsError = true;
                    _response.Message = "Too many login attempts. Please try again later.";
                    return _response;
                }
            }

            using IDbConnection db = _dbFactory.Open();
            YMU01 user = db.Single<YMU01>(u => u.U01F02 == loginDto.U01102 && u.U01F03 == HashPassword(loginDto.U01103));

            if (user == null)
            {
                _loginAttempts[loginDto.U01102] = (_loginAttempts.ContainsKey(loginDto.U01102) ? attempts.RequestCount + 1 : 1, DateTime.UtcNow);
                _response.IsError = true;
                _response.Message = "Invalid username or password.";
                return _response;
            }

            _loginAttempts.Remove(loginDto.U01102);

            // Generate JWT token here
            string token = GenerateJwtToken(user.U01F01, user.U01F04);

            _response.Message = "Login successful.";
            _response.Data = new
            {
                Token = token,
                UserId = user.U01F01,
                Username = user.U01F02,
                Role = user.U01F04
            };
            return _response;
        }

        /// <summary>
        /// Generates a JWT token for the authenticated user.
        /// </summary>
        public string GenerateJwtToken(int userId, string role)
        {
            byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
