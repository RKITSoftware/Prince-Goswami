using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public class JokeResultFilter : IAsyncResultFilter
    {
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
    }
}
