using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilterDemo.Filters
{
    // <summary>
    /// Below class is customize authorization filter
    /// IAuthorizationFilter : this interface use to build custom authorization filter
    /// Attribute : so we can use this filter as attribute
    /// </summary>
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public AuthorizationFilter(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var key = Encoding.UTF8.GetBytes("YourSuperSecretKey"); // Move this to configuration
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (principal == null || !principal.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == _role))
                {
                    context.Result = new ForbidResult();
                }
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
