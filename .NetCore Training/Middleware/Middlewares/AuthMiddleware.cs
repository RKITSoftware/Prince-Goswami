using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BasicAuthenticationMiddleware> _logger;

        public BasicAuthenticationMiddleware(RequestDelegate next, ILogger<BasicAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            Console.WriteLine("Start of Authentication ");
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                // Extract credentials from the Authorization header
                string encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                byte[] decodedBytes = Convert.FromBase64String(encodedUsernamePassword);
                string decodedCredentials = Encoding.UTF8.GetString(decodedBytes).Split(':', 2)[0];
                
                    // Validate credentials (this is a placeholder for actual authentication logic)
                    if (IsAuthenticated(decodedCredentials))
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
        private bool IsAuthenticated(string userName)
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
