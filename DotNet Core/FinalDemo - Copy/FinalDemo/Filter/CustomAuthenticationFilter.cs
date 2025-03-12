using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalDemo.Filter
{
    /// <summary>
    /// Custom authentication filter for validating JWT tokens.
    /// Ensures that incoming requests contain a valid authorization token.
    /// </summary>
    public class CustomAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        private readonly ILogger<CustomAuthenticationFilter> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthenticationFilter"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging authentication events.</param>
        /// <param name="configuration">Configuration instance for accessing JWT settings.</param>
        public CustomAuthenticationFilter(ILogger<CustomAuthenticationFilter> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Executes before the action method is called.
        /// Extracts and validates the JWT token from the request headers.
        /// </summary>
        /// <param name="context">The authorization filter context.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;

            // Extract token from headers
            if (!httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.Result = new UnauthorizedObjectResult(new { Message = "Missing Authorization header." });
                return;
            }

            var token = authHeader.ToString().Replace("Bearer ", "");
            var user = ValidateToken(token);

            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult(new { Message = "Invalid or expired token." });
                return;
            }

            // Store user details in HttpContext for further use
            httpContext.Items["UserId"] = user.UserId;
            httpContext.Items["Role"] = user.Role;
        }

        /// <summary>
        /// Validates a JWT token and extracts user information.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>Returns an authenticated user if valid, otherwise null.</returns>
        private AuthenticatedUser ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyString = _configuration["Jwt:Key"];
                if (string.IsNullOrEmpty(keyString))
                {
                    _logger.LogError("JWT Key is missing in configuration.");
                    throw new InvalidOperationException("JWT Key is missing in configuration.");
                }
                var key = Encoding.UTF8.GetBytes(keyString);


                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var role = principal.FindFirst(ClaimTypes.Role)?.Value ?? "User";

                return new AuthenticatedUser(userId, role);
            }
            catch (Exception ex)
            {
                _logger.LogError("Token validation failed: {Message}", ex.Message);
                return null;
            }
        }
    }

    /// <summary>
    /// Represents an authenticated user.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user.</param>
    /// <param name="Role">The role assigned to the user.</param>
    public record AuthenticatedUser(int UserId, string Role);
}
