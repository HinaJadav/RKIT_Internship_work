using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomAuthorizationFilter : Attribute, IAuthorizationFilter
{
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
