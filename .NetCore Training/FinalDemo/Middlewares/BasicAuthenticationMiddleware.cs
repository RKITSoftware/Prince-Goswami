using FinalDemo.BL.Interface.Service;
using FinalDemo.Filters;
using FinalDemo.Models;
using FinalDemo.Models.POCO;
using NLog;
using NLog.Web;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using ZstdSharp.Unsafe;

namespace FinalDemo.Middlewares
{
    /// <summary>
    /// Middleware for basic authentication using username and password from Authorization header.
    /// </summary>
    public class BasicAuthenticationMiddleware : IMiddleware
    {
        #region Private Fields

        private readonly RequestDelegate _next;
        private readonly Logger _logger;
        private readonly IBLUSR01 _userService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthenticationMiddleware"/> class.
        /// </summary>
        /// <param name="userService">The user service used to authenticate users.</param>
        public BasicAuthenticationMiddleware(IBLUSR01 userService)
        {
            _userService = userService;
            _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Middleware invocation method to handle basic authentication.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <param name="next">The delegate representing the next middleware in the pipeline.</param>
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

                Response objResponse = _userService.AuthorizeUser(userName, password);
                
                if (objResponse.IsError)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
                USR01 user = objResponse.Data;
                // Creates claims for the authenticated user
                var identity = context.User.Identity as ClaimsIdentity;
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.R01F01.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.R01F04.ToString()));

                // Adds the claims identity to the request context user
                context.User.AddIdentity(identity);

                _logger.Info(string.Format(@"{0} | {1}", MethodBase.GetCurrentMethod().Name, user.R01F04.Value() + " Login : " + user.R01F02));

                await next(context);
            }
        }

        #endregion
    }

    /// <summary>
    /// Extension method used to add the BasicAuthenticationMiddleware to the HTTP request pipeline.
    /// </summary>
    public static class BasicAuthenticationMiddlewareExtensions
    {
        /// <summary>
        /// Adds the BasicAuthenticationMiddleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The application builder with the middleware added.</returns>
        public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
