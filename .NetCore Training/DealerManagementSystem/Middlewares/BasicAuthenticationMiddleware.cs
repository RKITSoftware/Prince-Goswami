using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.BL.Services;
using DealerManagementSystem.DAL;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagementSystem.Middlewares
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BasicAuthenticationMiddleware> _logger;
        private readonly IBLUSR01 _userService;

        public BasicAuthenticationMiddleware(BLUSR01 userService,RequestDelegate next, ILogger<BasicAuthenticationMiddleware> logger)
        {
            _userService = userService;
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                // Extracts and decodes username and password from Authorization header
                string header = context.Request.Headers["Authorization"].ToString();
                string credential = header.Substring(6);
                string encodingData = Encoding.UTF8.GetString(Convert.FromBase64String(credential));
                string[] userNamePass = encodingData.Split(':');
                string userName = userNamePass[0];
                string password = userNamePass[1];

                // Validate credentials (this is a placeholder for actual authentication logic)
                if (IsAuthenticated(userName,password))
                    {
                        // If authenticated, proceed to the next middleware
            Console.WriteLine("Authenticated");

                    await _next(context);
                        return;
                    }
            
                // If authentication fails, return a 401 Unauthorized response
            Console.WriteLine("Authentication Failed");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Your API\"";
                await context.Response.WriteAsync("Unauthorized");
            }
        }
        private bool IsAuthenticated(string userName, string password)
        {

            if (userName == "Admin")
            {
                return true;
            }
            return false;
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
