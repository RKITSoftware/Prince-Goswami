using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApiDemo.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Apply custom logic before the request reaches the endpoint
            Console.WriteLine("Custom filter middleware: Before handling the request");

            // Call the next middleware in the pipeline
            await _next(context);

            // Apply custom logic after the response is generated
            Console.WriteLine("Custom filter middleware: After handling the response");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline
    public static class CustomFilterMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
