using Microsoft.AspNetCore.Mvc;
using Filter.Filters;
using System;

namespace Filter.Controllers
{
    /// <summary>
    /// Controller for fetching jokes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(JokeActionFilter))]
    [ServiceFilter(typeof(JokeResultFilter))]
    [ServiceFilter(typeof(JokeExceptionFilter))]
    [TypeFilter(typeof(JokeResourceFilter))]
    public class JokeController : ControllerBase
    {
        /// <summary>
        /// Gets a joke.
        /// </summary>
        /// <returns>The joke response.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            // Return a sample response
            return Ok("This is your response!");
        }

        /// <summary>
        /// Throws an exception.
        /// </summary>
        /// <returns>An exception.</returns>
        [HttpGet]
        [Route("Error")]
        public IActionResult Error()
        {
            throw new Exception("An Exception occurred!!");
        }
    }
}
