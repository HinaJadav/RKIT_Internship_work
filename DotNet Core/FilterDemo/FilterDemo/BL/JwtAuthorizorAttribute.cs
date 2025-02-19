using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilterDemo.BL
{
    /// <summary>
    /// Custom JWT authorization filter that validates JWT tokens in incoming requests.
    /// Ensures that only authenticated users with valid tokens can access protected endpoints.
    /// </summary>
    public class JwtAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        // Secret key used for signing and validating JWT tokens
        private static readonly string SecretKey = "9c1a19a1bc0ed85788a01293fa94d483ba05c882020fe1bef6a7ab0e1911bd2d!";

        /// <summary>
        /// Called during the authorization phase to validate the JWT token.
        /// </summary>
        /// <param name="context">The authorization filter context.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            // Check if Authorization header is missing or improperly formatted
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new JsonResult(new { message = "Authorization header is missing or invalid." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            // Validate the JWT token
            if (!ValidateToken(token, out ClaimsPrincipal claimsPrincipal))
            {
                context.Result = new JsonResult(new { message = "Invalid or expired token." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            // Assign the validated claims to the current user context
            context.HttpContext.User = claimsPrincipal;
        }

        /// <summary>
        /// Validates the JWT token and extracts user claims.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <param name="claimsPrincipal">The claims principal extracted from the token.</param>
        /// <returns>True if the token is valid; otherwise, false.</returns>
        private bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false, // Issuer validation is disabled for simplicity
                    ValidateAudience = false, // Audience validation is disabled for simplicity
                    IssuerSigningKey = key, // The secret key used for signing
                    ValidateLifetime = true // Ensures the token is not expired
                }, out SecurityToken validatedToken);

                claimsPrincipal = principal;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
