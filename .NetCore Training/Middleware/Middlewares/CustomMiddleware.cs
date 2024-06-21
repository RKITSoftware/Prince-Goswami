namespace WebApiDemo.Middlewares
{
    /// <summary>
    /// Middleware for applying custom logic before and after handling HTTP requests.
    /// </summary>
    public class CustomMiddleware
    {
        #region Private Fields
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        /// <summary>
        /// Invokes the middleware to apply custom logic before and after handling the request.
        /// </summary>
        /// <param name="context">The HttpContext for the request.</param>
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
        /// <summary>
        /// Adds the CustomMiddleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> instance.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
