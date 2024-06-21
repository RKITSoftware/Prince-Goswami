using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FinalDemo.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions and returning appropriate JSON error responses.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The delegate representing the next middleware in the pipeline.</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware to handle exceptions.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        #region Private Methods

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Customize your error message based on the exception type
            string errorMessage = exception switch
            {
                InvalidOperationException => "Invalid operation occurred",
                ArgumentException => "Invalid argument provided",
                // Add more cases for specific exception types if needed
                _ => "An unexpected error occurred"
            };

            var result = JsonConvert.SerializeObject(new { error = errorMessage });
            return context.Response.WriteAsync(result);
        }

        #endregion
    }

    /// <summary>
    /// Extension method used to add the ExceptionMiddleware to the HTTP request pipeline.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Adds the ExceptionMiddleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
