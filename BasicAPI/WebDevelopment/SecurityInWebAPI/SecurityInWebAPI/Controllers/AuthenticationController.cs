using SecurityInWebAPI.Models;
using System.Web.Http;
using SecurityInWebAPI.Handlers;

namespace SecurityInWebAPI.Controllers
{
    public class AuthenticationController : ApiController
    {
        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="model">The login credentials (e.g., username and password).</param>
        /// <returns>A JWT token if authentication is successful.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginModel model)
        {
            if (IsValidUser(model.Username, model.Password))
            {
                // Generate JWT token for valid user
                var token = JwtTokenManager.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Validates a user's credentials.
        /// For simplicity, this is just a mock method. Replace with actual authentication logic.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the credentials are valid, otherwise false.</returns>
        private bool IsValidUser(string username, string password)
        {
            // Example mock validation. Replace with actual logic.
            return username == "admin" && password == "password";
        }
    }
}