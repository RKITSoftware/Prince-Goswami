using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public class JokeExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new ObjectResult("An error occurred while processing your request.")
            {
                StatusCode = 500
            };

            await Task.CompletedTask;
        }
    }
}
