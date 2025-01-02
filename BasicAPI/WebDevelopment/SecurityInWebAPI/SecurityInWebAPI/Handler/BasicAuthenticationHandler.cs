using System;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class BasicAuthenticationHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authHeader = request.Headers.Authorization;

        if (authHeader != null && authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
        {
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
            var username = credentials[0];
            var password = credentials[1];

            if (IsAuthorized(username, password))
            {
                var identity = new GenericIdentity(username);
                Thread.CurrentPrincipal = new GenericPrincipal(identity, null);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private bool IsAuthorized(string username, string password)
    {
        // Replace with your authentication logic
        return username == "admin" && password == "password";
    }
}
