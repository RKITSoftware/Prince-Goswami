using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.BL.Services;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Filters;
using NLog;
using NLog.Web;
using System;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagementSystem.Middlewares
{
    public class BasicAuthenticationMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Logger _logger;
        private readonly IBLUSR01 _userService;

        public BasicAuthenticationMiddleware(IBLUSR01 userService)
        {
            _userService = userService;
            _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //// Checks if the endpoint allows anonymous access
            //bool hasAllowAnonymous = context.GetEndpoint().Metadata.Any(meta => meta.GetType() == typeof(AllowAnonymousAttribute));

            //// If endpoint allows anonymous access, continue to next middleware
            //if (hasAllowAnonymous)
            //{
            //    await next(context);
            //    return;
            //}

            // If Authorization header is missing, return Unauthorized status code
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            string authHeader = context.Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                // Extracts and decodes username and password from Authorization header
                string credential = authHeader.Substring(6);
                string encodingData = Encoding.UTF8.GetString(Convert.FromBase64String(credential));
                string[] userNamePass = encodingData.Split(':');
                string userName = userNamePass[0];
                string password = userNamePass[1];


                var user = _userService.AuthorizeUser(userName,password);

                if (user == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }

                // Creates claims for the authenticated user
                var identity = context.User.Identity as ClaimsIdentity;
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.R01F01.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.R01F04.ToString()));


                // Adds the claims identity to the request context user
                context.User.AddIdentity(identity);

                _logger.Info(String.Format(@"{0} | {1}", MethodBase.GetCurrentMethod().Name, EnumConverter.GetRoleFullName(user.R01F04) + " Login : " + user.R01F02));

                await next(context);
            }
        }

      
    }

    // Extension method used to add the middleware to the HTTP request pipeline
    public static class BasicAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
