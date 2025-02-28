using FinalDemo.BL.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTOs;
using FinalDemo.Models.Enums;
using FinalDemo.Models.POCOs;
using Microsoft.IdentityModel.Tokens;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Provides user-related services, including registration, authentication, and user management.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IDbConnection _db;
        private readonly ILogger<UserService> _logger;
        private YMU01 _userObj;
        private int _userId;
        private Response _response;
        public OperationType Type { get; set; }
        private readonly IConfiguration _configuration;
        private static readonly Dictionary<string, (int RequestCount, DateTime LastRequestTime)> _loginAttempts = new();
        private const int MAX_LOGIN_ATTEMPTS = 5;
        private static readonly TimeSpan LOGIN_ATTEMPT_RESET_TIME = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService(IConfiguration configuration, IDbConnection db, ILogger<UserService> logger)
        {
            _db = db;
            _logger = logger;
            _configuration = configuration;
            _response = new Response();
        }

        /// <summary>
        /// Hashes a given password using SHA-256.
        /// </summary>
        private string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        /// <summary>
        /// Converts a DTO user object to a POCO user object.
        /// </summary>
        public YMU01 ToPocoUser(DTOYMU01 dtoUser)
        {

            return new YMU01
            {
                U01F02 = dtoUser.U01102,
                U01F03 = this.HashPassword(dtoUser.U01103),
                U01F04 = dtoUser.U01104
            };
        }

        /// <summary>
        /// Converts a POCO user object to a DTO response object.
        /// </summary>
        private DTOResponse ToDTOUser(YMU01 user)
        {
            return new DTOResponse
            {
                U01102 = user.U01F02,
                U01104 = user.U01F04,
            };
        }

        /// <summary>
        /// Prepares a user for saving, either creating a new user or updating an existing one.
        /// </summary>
        public void PreSaveUser(DTOYMU01 dtoUser, OperationType operationType)
        {
            Type = operationType;

            Console.WriteLine($"Received SignUp DTO: {Newtonsoft.Json.JsonConvert.SerializeObject(dtoUser)}");

            int userId = dtoUser.U01101;
            if (Type == OperationType.E)
            {
                _userId = userId;
                YMU01 existingUser = _db.SingleById<YMU01>(_userId);

                if (existingUser == null)
                {
                    throw new KeyNotFoundException($"User with ID {_userId} not found.");
                }

                _userObj = existingUser;
                _userObj.U01F02 = dtoUser.U01102 ?? _userObj.U01F02;
                _userObj.U01F03 = string.IsNullOrEmpty(dtoUser.U01103) ? _userObj.U01F03 : HashPassword(dtoUser.U01103);
                _userObj.U01F04 = dtoUser.U01104 ?? _userObj.U01F04;
            }
            else if(Type == OperationType.A)
            {
                YMU01 newUser = ToPocoUser(dtoUser);

                if (newUser == null)
                {
                    throw new ArgumentException("Conversion to POCO failed.");
                }

                _userObj = newUser;
                _userId = _userObj.U01F01; // Assign `_userId` from newly created user (if available)

                Console.WriteLine($"New User Object: {Newtonsoft.Json.JsonConvert.SerializeObject(newUser)}");
            }
        }

        /// <summary>
        /// Prepares a user object for login validation.
        /// </summary>
        public void PreLoginUser(DTOLogin dtoUser)
        {
            dtoUser.U01103 = HashPassword(dtoUser.U01103);
            _userObj = dtoUser.ConvertTo<YMU01>();
        }

        /// <summary>
        /// Checks if a user exists in the database.
        /// </summary>
        private bool IsUserExist(int userId)
        {
            Console.WriteLine(userId);

            var user = _db.Single<YMU01>(x => x.U01F01 == _userId);
            Console.WriteLine(user != null ? $"User found: {user.U01F01}" : "User not found");

            return _db.Count<YMU01>(x => x.U01F01 == userId) > 0;
        }


        /// <summary>
        /// Prepares a user for deletion by validating their existence and setting the operation type.
        /// </summary>
        public void PreDeleteUser(int userId, OperationType operationType)
        {
            // Validate the ID
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID. ID must be greater than 0.");
            }

            // Check if the user exists in the database
            YMU01 existingUser = _db.SingleById<YMU01>(userId);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"No user found with ID {userId}.");
            }

            // Assign values if validation passes
            Type = operationType;
            _userId = userId;
        }

        /// <summary>
        /// Validates the user object before processing.
        /// </summary>
        public Response Validation()
        {
            // Console.WriteLine($"aa: {IsUserExist(_userId)}");

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
        /// Saves the user object to the database.
        /// </summary>
        public Response Save()
        {
            try
            {
                if (_userObj == null)
                {
                    _response.IsError = true;
                    _response.Message = "User object is null before save.";
                    _logger.LogError("User object is null before save.");
                    return _response;
                }

                _logger.LogInformation("Preparing to save user: {@UserObj}", _userObj);

                if (Type == OperationType.A)
                {
                    _logger.LogInformation("Attempting to insert user...");
                    _db.Insert(_userObj);
                    _logger.LogInformation("User inserted successfully: {@UserObj}", _userObj);
                    _response.Message = "User registered successfully.";
                    _response.Data = _userObj;
                }
                else if (Type == OperationType.E)
                {
                    if (!IsUserExist(_userId))
                    {
                        _response.IsError = true;
                        _response.Message = "User not found.";
                        _logger.LogError("User not found for update: UserID = {UserId}", _userId);
                        return _response;
                    }

                    _logger.LogInformation("Updating user...");
                    _db.Update(_userObj);
                    _logger.LogInformation("User updated successfully.");
                    _response.Message = "User details updated successfully.";
                }
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
                _logger.LogError(ex, "Error while saving user");
            }
            return _response;
        }



        /// <summary>
        /// Deletes the user object to the database.
        /// </summary>
        public Response Delete()
        {
            try
            {
                _db.DeleteById<YMU01>(_userId);
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
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A DTOResponse containing user details.</returns>
        /// <exception cref="ArgumentException">Thrown when the provided ID is invalid (less than or equal to 0).</exception>
        /// <exception cref="KeyNotFoundException">Thrown when no user is found with the given ID.</exception>
        public DTOResponse GetById(int id)
        {
            // Validate the ID
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID. ID must be greater than 0.");
            }

            // Fetch the user from the database
            YMU01 user = _db.SingleById<YMU01>(id);

            // Check if the user exists
            if (user == null)
            {
                throw new KeyNotFoundException($"No user found with ID {id}.");
            }

            // Convert to DTO and return
            return ToDTOUser(user);
        }


        /// <summary>
        /// Authenticates a user and generates a JWT token upon successful login.
        /// </summary>
        /// <param name="loginDto">The login details containing username and password.</param>
        /// <returns>A response containing the authentication result and token if successful.</returns>
        public Response Login(DTOLogin loginDto)
        {
            // Check if the user has exceeded the maximum login attempts
            if (_loginAttempts.TryGetValue(loginDto.U01102, out var attempts))
            {
                if (attempts.RequestCount >= MAX_LOGIN_ATTEMPTS && DateTime.UtcNow - attempts.LastRequestTime < LOGIN_ATTEMPT_RESET_TIME)
                {
                    _response.IsError = true;
                    _response.Message = "Too many login attempts. Please try again later.";
                    return _response;
                }
            }

            // Validate user credentials against the database
            YMU01 user = _db.Single<YMU01>(u => u.U01F02 == loginDto.U01102 && u.U01F03 == HashPassword(loginDto.U01103));

            // If user not found, increment login attempt counter and return an error response
            if (user == null)
            {
                _loginAttempts[loginDto.U01102] = (_loginAttempts.ContainsKey(loginDto.U01102) ? attempts.RequestCount + 1 : 1, DateTime.UtcNow);
                _response.IsError = true;
                _response.Message = "Invalid username or password.";
                return _response;
            }

            // Successful login, remove failed login attempts tracking
            _loginAttempts.Remove(loginDto.U01102);

            // Generate JWT token for authenticated user
            string token = GenerateJwtToken(user.U01F01, user.U01F04);

            // Return success response with user details and token
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
        /// Generates a JWT token for authentication and authorization.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="role">The role assigned to the user.</param>
        /// <returns>A signed JWT token as a string.</returns>
        public string GenerateJwtToken(int userId, string role)
        {
            // Retrieve the secret key from the configuration
            byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            // Define claims for the token (User ID and Role)
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim(ClaimTypes.Role, role)
    };

            // Configure token properties including expiration, signing credentials, issuer, and audience
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            // Generate the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the token as a string
            return tokenHandler.WriteToken(token);
        }

    }
}
