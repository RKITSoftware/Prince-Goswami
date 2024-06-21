using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Filter.Filters
{
    /// <summary>
    /// Adds a programming joke to the response content if available in the response headers.
    /// </summary>
    public class JokeResultFilter : IAsyncResultFilter
    {
        #region Public Methods

        /// <summary>
        /// Executes the result asynchronously, adding a programming joke to the response content if available.
        /// </summary>
        /// <param name="context">The context for the action result execution.</param>
        /// <param name="next">The delegate representing the remaining result execution pipeline.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is string)
            {
                // Fetch the joke from the response headers
                if (context.HttpContext.Response.Headers.TryGetValue("X-Joke", out var joke))
                {
                    // Append the joke to the response content
                    objectResult.Value += $"\n\nProgramming Joke: {joke}";
                }
            }

            await next();
        }

        #endregion
    }
}
