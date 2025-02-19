using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

/// <summary>
/// Custom authorization filter that restricts access based on authentication and role.
/// Users must be authenticated, and only those with the "Admin" role are allowed access.
/// Unauthorized users receive a 401 status, while non-admins receive a 403 status.
/// </summary>
public class CustomAuthorizationFilter : Attribute, IAuthorizationFilter
{
    // It gets called automatically during the authorization phase of the request pipeline.
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        // Check if the user is authenticated
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return;
        }

        // Check if the user has the "Admin" role
        if (!user.IsInRole("Admin"))
        {
            context.Result = new JsonResult(new { message = "Forbidden: Admins only" })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}
