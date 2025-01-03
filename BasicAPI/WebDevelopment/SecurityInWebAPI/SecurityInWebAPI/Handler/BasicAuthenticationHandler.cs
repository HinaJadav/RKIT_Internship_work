using System;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// A custom HTTP message handler for Basic Authentication.
/// This handler validates the credentials passed in the Authorization header
/// and sets the current principal for authorization purposes.
/// </summary>
public class BasicAuthenticationHandler : DelegatingHandler
{
    /// <summary>
    /// Handles the HTTP request by validating the Authorization header.
    /// </summary>
    /// <param name="request">The incoming HTTP request.</param>
    /// <param name="cancellationToken">Token to monitor for request cancellation.</param>
    /// <returns>A response message indicating success or failure of the authentication.</returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authHeader = request.Headers.Authorization;

        if (authHeader != null && authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
        {
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
            var username = credentials[0];
            var password = credentials[1];

            Console.WriteLine($"Username: {username}, Password: {password}");

            if (IsAuthorized(username, password, out string[] roles))
            {
                var identity = new GenericIdentity(username);
                Thread.CurrentPrincipal = new GenericPrincipal(identity, roles);
                Console.WriteLine("Authorization successful.");
            }
            else
            {
                Console.WriteLine("Authorization failed.");
                return request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
            }
        }
        else
        {
            Console.WriteLine("No Authorization header found.");
            return request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
        }

        return await base.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Validates the username and password and assigns roles for authorization.
    /// </summary>
    /// <param name="username">The username to validate.</param>
    /// <param name="password">The password to validate.</param>
    /// <param name="roles">An output parameter for the roles associated with the user.</param>
    /// <returns>True if the user is authorized; otherwise, false.</returns>
    private bool IsAuthorized(string username, string password, out string[] roles)
    {
        roles = null;

        // Simulated user credentials validation (this should be replaced with a database or external service)
        if (username == "admin" && password == "admin")
        {
            roles = new[] { "Admin" };  // Assign the "Admin" role to the user
            return true;
        }
        else if (username == "Nahi" && password == "Nahi")
        {
            roles = new[] { "Users" };  // Assign the "Users" role to the user
            return true;
        }

        return false; // Unauthorized if username/password combination is incorrect
    }
}
