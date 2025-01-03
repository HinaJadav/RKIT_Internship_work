using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http;

namespace SecurityInWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // JWT Bearer Token authentication
            var secretKey = Encoding.UTF8.GetBytes("ThisIsASecretKeyThatIsAtLeast32Characters!");

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "yourIssuer", // Must match JwtTokenManager
                    ValidateAudience = true,
                    ValidAudience = "yourAudience", // Must match JwtTokenManager
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                }
            });

            // Register Web API routes
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
