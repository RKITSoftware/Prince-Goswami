using Microsoft.AspNetCore.Mvc;
using Filter.Filters;

namespace Filter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(JokeActionFilter))]
    [ServiceFilter(typeof(JokeResultFilter))]
    [ServiceFilter(typeof(JokeExceptionFilter))]
    public class JokeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Return a sample response
            return Ok("This is your response!");
        }

        [HttpGet]
        [Route("Error")]
        public IActionResult Error()
        {
            throw new Exception("An Exception occured!!");
            // Return a sample response
        }
    }
}
