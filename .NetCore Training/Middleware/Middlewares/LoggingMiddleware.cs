using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Capture information about the incoming request
            string requestMethod = context.Request.Method;
            string requestPath = context.Request.Path;
            //string requestBody = await GetRequestBodyAsync(context.Request);

            _logger.LogInformation($"Incoming Request: {requestMethod} {requestPath}");

            // Proceed to the next middleware in the pipeline
            await _next(context);

            // Capture information about the outgoing response
            int responseStatusCode = context.Response.StatusCode;
            //string responseBody = await GetResponseBodyAsync(context.Response);

            _logger.LogInformation($"Outgoing Response: {responseStatusCode}");
        }

        private async Task<string> GetRequestBodyAsync(HttpRequest request)
        {
            // Read and return the request body
            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private async Task<string> GetResponseBodyAsync(HttpResponse response)
        {
            // Capture the response body if it's a non-redirect status code
            if (response.StatusCode >= 200 && response.StatusCode < 300)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await response.Body.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(memoryStream, Encoding.UTF8))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            else
            {
                // If the response is a redirect or error, just return the status code
                return $"Response status code: {response.StatusCode}";
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}