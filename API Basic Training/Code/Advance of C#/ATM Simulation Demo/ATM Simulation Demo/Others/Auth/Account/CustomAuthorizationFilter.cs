using System.Net;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Http;
using System.Linq;
using System;

namespace ATM_Simulation_Demo.Others.Auth.Account
{
    public class CustomAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
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
            
            TokenManager.setSessionId(token);
        }

        //private int ExtractExpectedAccountId(HttpActionContext actionContext)
        //{
        //    // Implement logic to extract the expected user ID from the action context or other sources
        //    // For example, you might retrieve it from route parameters, query string, headers, etc.
        //    // Replace this with your actual logic based on your application requirements.
        //    // For illustration purposes, assuming it's present in the query string as "userId"
        //    var userIdQueryParam = actionContext.Request.GetQueryNameValuePairs().FirstOrDefault(q => q.Key.Equals("userId", StringComparison.OrdinalIgnoreCase));
        //    if (!string.IsNullOrEmpty(userIdQueryParam.Value) && int.TryParse(userIdQueryParam.Value, out int accountId))
        //    {
        //        return accountId;
        //    }

        //    // If user ID is not found or cannot be parsed, return a default value or handle accordingly
        //    return -1; // Replace with your default value or appropriate handling
        //}
    }
}