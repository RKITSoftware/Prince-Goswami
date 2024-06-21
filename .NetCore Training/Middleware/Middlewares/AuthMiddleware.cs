using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    // Middleware for basic authentication
    public class BasicAuthenticationMiddleware
    {
        #region private fields
        private readonly RequestDelegate _next;
        private readonly ILogger<BasicAuthenticationMiddleware> _logger;
        #endregion

        #region constructor
        public BasicAuthenticationMiddleware(RequestDelegate next, ILogger<BasicAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion

        // Method to handle incoming HTTP requests
        public async Task Invoke(HttpContext context)
        {
            // Check if the request path requires authentication
            if (!context.Request.Path.StartsWithSegments("/Auth/token"))
            {
                string authHeader = context.Request.Headers["Authorization"];
                Console.WriteLine("Start of Authentication ");
                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    // Extract credentials from the Authorization header
                    string encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                    byte[] decodedBytes = Convert.FromBase64String(encodedUsernamePassword);
                    string[] decodedCredentials = Encoding.UTF8.GetString(decodedBytes).Split(':', 2);
                    string userName = decodedCredentials[0];
                    string password = decodedCredentials[1];

                    // Validate credentials (placeholder for actual authentication logic)
                    if (IsAuthenticated(userName, password))
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
            else
                await _next(context);
        }

        /// <summary>
        /// Method to validate user credentials
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        private bool IsAuthenticated(string userName, string password)
        {
            if (userName.Equals("Admin") && password.Equals("Password"))
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline
    /// </summary>
    public static class BasicAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
