using System.Net;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Http;
using System.Linq;

/// <summary>
/// Custom authorization filter that checks whether the request has the required roles.
/// </summary>
public class CustomAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
{
    /// <summary>
    /// Called when authorization is required for an HTTP request.
    /// </summary>
    /// <param name="actionContext">The context for the action.</param>
    public void OnAuthorization(HttpActionContext actionContext)
    {
        // Retrieve the user principal from the request context
        var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

        // Check if the user is not authenticated
        if (principal == null || !principal.Identity.IsAuthenticated)
        {
            // Return Unauthorized response
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized access");
            return;
        }

        // Retrieve required roles from the attribute
        var rolesRequired = Roles.Split(',');

        // Check if the user has any of the required roles
        if (rolesRequired.Any() && !rolesRequired.Any(role => principal.IsInRole(role)))
        {
            // Return Forbidden response if the user lacks required roles
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Insufficient permissions");
        }
    }
}
