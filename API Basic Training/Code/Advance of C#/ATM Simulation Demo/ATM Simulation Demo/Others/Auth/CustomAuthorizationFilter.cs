using System.Net;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Http;
using System.Linq;
using System;

namespace ATM_Simulation_Demo.Others.Auth
{
    /// <summary>
    /// Custom authorization filter for handling authorization in Web API requests.
    /// </summary>
    public class CustomAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Performs authorization logic for the HTTP action context.
        /// </summary>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the request has the Authorization header
            var authorizationHeader = actionContext.Request.Headers.Authorization;

            if (authorizationHeader == null || authorizationHeader.Scheme.ToLower() != "bearer")
            {
                // No Bearer token present, return Unauthorized
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized access");
                return;
            }

            // Extract the token from the Authorization header
            string token = authorizationHeader.Parameter;

            // Retrieve the user principal from the token
            var principal = TokenManager.GetPrincipal(token);

            // Check if the user is not authenticated
            if (principal == null || !principal.Identity.IsAuthenticated)
            {
                // Return Unauthorized response
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized access");
                return;
            }

            // Set the session ID in the token manager
            TokenManager.setSessionId(token);
        }
    }
}
