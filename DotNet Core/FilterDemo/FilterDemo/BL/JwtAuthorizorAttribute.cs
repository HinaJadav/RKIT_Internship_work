using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilterDemo.BL
{
    public class JwtAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private static readonly string SecretKey = "9c1a19a1bc0ed85788a01293fa94d483ba05c882020fe1bef6a7ab0e1911bd2d!";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new JsonResult(new { message = "Authorization header is missing or invalid." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            if (!ValidateToken(token, out ClaimsPrincipal claimsPrincipal))
            {
                context.Result = new JsonResult(new { message = "Invalid or expired token." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            context.HttpContext.User = claimsPrincipal;
        }

        private bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = key,
                    ValidateLifetime = true
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
