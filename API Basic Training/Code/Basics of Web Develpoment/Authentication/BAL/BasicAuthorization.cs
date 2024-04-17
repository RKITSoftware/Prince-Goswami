using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Authentication.BAL
{
    ///<summary>
    /// Provides basic authorization functionality by checking if the user is authenticated.
    ///</summary>
    public class BasicAuthorization : AuthorizeAttribute
    {
        ///<summary>
        /// Handles the unauthorized request by returning a forbidden status code if the user is not authenticated.
        ///</summary>
        ///<param name="actionContext">The context for the action.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // If the user is authenticated, handle the unauthorized request
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                // If the user is not authenticated, return a forbidden status code
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}
