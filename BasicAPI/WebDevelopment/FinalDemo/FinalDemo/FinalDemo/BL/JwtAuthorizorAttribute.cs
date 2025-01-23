using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FinalDemo.BL
{
    /// <summary>
    /// Custom authorization filter to validate JWT tokens in incoming HTTP requests.
    /// </summary>
    public class JwtAuthorizorAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// This method is called to authorize a request by checking if the request contains a valid JWT token.
        /// </summary>
        /// <param name="actionContext">The context of the action that is being executed.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the request contains an Authorization header
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                // If the Authorization header is missing or not using Bearer scheme, return Unauthorized response
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized, "Authorization header is missing or invalid.");
                return;
            }

            var token = authHeader.Parameter;

            // Validate the token (this is just a placeholder; replace with your logic)
            if (!ValidateToken(token))
            {
                // If the token is invalid or expired, return Unauthorized response
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized, "Invalid or expired token.");
                return;
            }

            // If valid, set the principal (optional)
            var claimsPrincipal = GetPrincipalFromToken(token);
            if (claimsPrincipal != null)
            {
                // If token claims are valid, set the current principal for the request context
                actionContext.RequestContext.Principal = claimsPrincipal;
            }
            else
            {
                // If the claims are invalid, return Unauthorized response
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized, "Invalid token claims.");
            }
        }

        /// <summary>
        /// Validates the JWT token by verifying its signature and checking expiration.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>True if the token is valid, otherwise false.</returns>
        private bool ValidateToken(string token)
        {
            var secretKey = "9c1a19a1bc0ed85788a01293fa94d483ba05c882020fe1bef6a7ab0e1911bd2d!"; // Secret key for validating the token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validate the token using the signing key, issuer, and audience (can be configured as needed)
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false, // Set to true if you have an issuer
                    ValidateAudience = false, // Set to true if you have an audience
                    IssuerSigningKey = key
                }, out SecurityToken validatedToken);

                // Return true if the token is successfully validated
                return validatedToken != null;
            }
            catch
            {
                // If validation fails, return false
                return false;
            }
        }

        /// <summary>
        /// Extracts claims from the JWT token and returns a ClaimsPrincipal.
        /// </summary>
        /// <param name="token">The JWT token to extract claims from.</param>
        /// <returns>A ClaimsPrincipal containing the claims from the token.</returns>
        private System.Security.Claims.ClaimsPrincipal GetPrincipalFromToken(string token)
        {            // Example: Parse token and create claims (replace with real implementation)
            var identity = new System.Security.Claims.ClaimsIdentity(
                new[] { new System.Security.Claims.Claim("token", token) },
                "Bearer");
            return new System.Security.Claims.ClaimsPrincipal(identity);
        }
    }

}
