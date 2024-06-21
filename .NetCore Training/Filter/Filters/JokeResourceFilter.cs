using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Filter.Filters
{
    /// <summary>
    /// Caches responses based on request parameters.
    /// </summary>
    public class JokeResourceFilter : IAsyncResourceFilter
    {
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="JokeResourceFilter"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache service.</param>
        public JokeResourceFilter(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        #region Public Methods

        /// <summary>
        /// Executes asynchronously before and after the execution of a resource.
        /// </summary>
        /// <param name="context">The context for the resource execution.</param>
        /// <param name="next">The delegate representing the remaining resource execution pipeline.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            // Generate a unique key based on request parameters
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);

            // Try to get the data from the cache
            if (_cache.TryGetValue(cacheKey, out string cachedData))
            {
                // If data exists in cache, return it
                context.Result = new ContentResult
                {
                    Content = cachedData,
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "text/plain"
                };
                return;
            }

            var executedContext = await next();
            var result1 = executedContext.Result;
            // After execution, cache the result if appropriate
            if (executedContext.Result is ObjectResult result && result.StatusCode == StatusCodes.Status200OK)
            {
                var resultData = JsonSerializer.Serialize(result.Value);
                _cache.Set(cacheKey, resultData, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3000)
                });
            }
        }

        #endregion

        #region Private Methods

        // Helper method to generate a unique cache key based on request parameters
        private string GenerateCacheKey(HttpRequest request)
        {
            // Here, you can generate a unique key based on request parameters like route, query string, etc.
            // For simplicity, let's just use the request path as the cache key
            return request.Path.ToString();
        }

        #endregion
    }
}
