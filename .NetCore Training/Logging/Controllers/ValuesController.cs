using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Logging.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleController : ControllerBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public IActionResult Get()
        {
            _logger.Info("SampleController: Get method called");

            // ...

            return Ok();
        }
    }
}
