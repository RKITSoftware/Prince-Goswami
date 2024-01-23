using Authentication.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Authentication.SEC;
using System.Threading;
using System.Security.Principal;
using System.Security.Claims;

namespace Authentication.BAL
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null) {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                //username : password base64 attrinute
                //admin : admin

                string decodeAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken)); 
                string[] userNamePassword = decodeAuthToken.Split(':');
                string userName = userNamePassword[0];
                string password = userNamePassword[1];

                if(ValidateUser.isUser(userName, password))
                {
                    var user = ValidateUser.Login(userName, password);   
                    var identity = new GenericIdentity(userName, password);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    identity.AddClaim( new Claim(ClaimTypes.MobilePhone, user.Phone));
                    identity.AddClaim( new Claim("Id", Convert.ToString(user.Id)));

                    IPrincipal principal = new GenericPrincipal(identity, user.role.Split(','));

                    Thread.CurrentPrincipal = principal;
                    if( HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization Declined");
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
                }

            }
        }
    }
}