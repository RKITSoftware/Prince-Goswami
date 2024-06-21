using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Filter.Filters
{
    /// <summary>
    /// Handles exceptions that occur during request processing.
    /// </summary>
    public class JokeExceptionFilter : IAsyncExceptionFilter
    {
        #region Public Methods

        /// <summary>
        /// Handles exceptions asynchronously during request processing.
        /// </summary>
        /// <param name="context">The context for the exception.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new ObjectResult("An error occurred while processing your request.")
            {
                StatusCode = 500
            };

            await Task.CompletedTask;
        }

        #endregion
    }
}
