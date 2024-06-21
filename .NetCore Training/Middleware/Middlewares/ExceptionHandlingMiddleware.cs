using Newtonsoft.Json;

namespace Middleware.Middlewares
{
    /// <summary>
    /// Middleware for handling unhandled exceptions and returning JSON error responses.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        #region Private Fields
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        #endregion

        #region Constructor
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Invokes the middleware to handle exceptions and return JSON error responses.
        /// </summary>
        /// <param name="context">The HttpContext for the request.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            //// remove or use ex
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError("An unhandled exception occurred.");

                // Set the response status code
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Return a JSON error response
                context.Response.ContentType = "application/json";
                var response = new { error = "An unexpected error occurred." };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline
    public static class ExceptionHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Adds the ExceptionHandlingMiddleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> instance.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
