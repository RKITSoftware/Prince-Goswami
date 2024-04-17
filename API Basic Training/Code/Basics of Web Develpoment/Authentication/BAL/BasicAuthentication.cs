using Authentication.SEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;
using System.Security.Principal;
using System.Security.Claims;
using Authentication.Models;

namespace Authentication.BAL
{
    ///<summary>
    /// Provides basic authentication functionality by validating user credentials from the request headers.
    ///</summary>
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        ///<summary>
        /// Overrides the default authorization behavior to perform basic authentication.
        ///</summary>
        ///<param name="actionContext">The context for the action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                // No authorization header provided, return unauthorized response
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
            }
            else
            {
                // Authorization header provided
                string authToken = actionContext.Request.Headers.Authorization.Parameter;

                // Decode the base64 encoded username:password string
                string decodeAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string[] userNamePassword = decodeAuthToken.Split(':');
                string userName = userNamePassword[0];
                string password = userNamePassword[1];

                if (ValidateUser.isUser(userName, password))
                {
                    // User credentials are valid, create identity and principal
                    Employees user = ValidateUser.Login(userName, password);
                    var identity = new GenericIdentity(userName, password);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.Phone));
                    identity.AddClaim(new Claim("Id", Convert.ToString(user.Id)));

                    // Create a principal with the user's role(s)
                    IPrincipal principal = new GenericPrincipal(identity, user.role.Split(','));

                    // Set the current principal
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                    {
                        // Set the user for the current HttpContext (for web applications)
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        // If HttpContext is not available (e.g., in non-web applications), return unauthorized response
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization Declined");
                    }
                }
                else
                {
                    // User credentials are invalid, return unauthorized response
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
                }

            }
        }
    }
}
