using FinalDemo.BL.Interfaces;
using FinalDemo.DB;
using FinalDemo.Extension;
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

        public UserService(IConfiguration configuration, IDbConnection db, ILogger<UserService> logger)
        {
            _db = db;
            _logger = logger;
            _configuration = configuration;
            _response = new Response();
        }

        private string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public YMU01 ToPocoUser(DTOYMU01 dtoUser)
        {
            Console.WriteLine($"2Received SignUp DTO: {Newtonsoft.Json.JsonConvert.SerializeObject(dtoUser)}");

            return new YMU01
            {
                U01F02 = dtoUser.U01102,
                U01F03 = this.HashPassword(dtoUser.U01103),
                U01F04 = dtoUser.U01104
            };
        }

        private DTOResponse ToDTOUser(YMU01 user)
        {
            return new DTOResponse
            {
                U01102 = user.U01F02,
                //U01103 = user.U01F03,
                U01104 = user.U01F04,
            };
        }
        public void PreSaveUser(DTOYMU01 dtoUser, OperationType operationType, int? userId = null)
        {
            Type = operationType;

            Console.WriteLine($"2Received SignUp DTO: {Newtonsoft.Json.JsonConvert.SerializeObject(dtoUser)}");


            if (userId.HasValue)
            {
                _userId = userId.Value;
                YMU01 existingUser = _db.SingleById<YMU01>(_userId);

                if (existingUser == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }

                _userObj = existingUser;
                _userObj.U01F02 = dtoUser.U01102 ?? _userObj.U01F02;
                _userObj.U01F03 = string.IsNullOrEmpty(dtoUser.U01103) ? _userObj.U01F03 : HashPassword(dtoUser.U01103);
                _userObj.U01F04 = dtoUser.U01104 ?? _userObj.U01F04;
            }
            else
            {
                YMU01 newUser = ToPocoUser(dtoUser);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(newUser));

                if (newUser == null)
                {
                    throw new ArgumentException("Conversion to POCO failed.");
                }
                _userObj = newUser;
            }
        }

        public void PreLoginUser(DTOLogin dtoUser)
        {
            dtoUser.U01103 = HashPassword(dtoUser.U01103);
            _userObj = dtoUser.ConvertTo<YMU01>();
        }

        private bool IsUserExist(int userId)
        {
            return _db.Exists<YMU01>(userId);
        }

        /*public void PreDeleteUser(int userId, OperationType operationType)
        {
            Type = operationType;
            _userId = userId;
        }*/

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

        /*public Response Save()
        {
            try
            {
                if (Type == OperationType.A)
                {
                    _logger.LogInformation("Saving user: {@UserObj}", _userObj);

                    _db.Insert(_userObj);
                    
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

                    _logger.LogInformation("Saving user: {@UserObj}", _userObj);

                    _db.Update(_userObj);
                    _response.Message = "User details updated successfully.";
                }
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
            }
            return _response;
        }*/

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

        /* public DTOResponse GetById(int id)
         {
             YMU01 user = _db.SingleById<YMU01>(id);
             //return user.ToDto<DTOYMU01>();
             return ToDTOUser(user);
         }*/

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

            YMU01 user = _db.Single<YMU01>(u => u.U01F02 == loginDto.U01102 && u.U01F03 == HashPassword(loginDto.U01103));

            if (user == null)
            {
                _loginAttempts[loginDto.U01102] = (_loginAttempts.ContainsKey(loginDto.U01102) ? attempts.RequestCount + 1 : 1, DateTime.UtcNow);
                _response.IsError = true;
                _response.Message = "Invalid username or password.";
                return _response;
            }

            _loginAttempts.Remove(loginDto.U01102);

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
