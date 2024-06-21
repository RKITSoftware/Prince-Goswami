using Logging.BL;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Logging.Controllers
{

    /// <summary>
    /// Controller for handling sample requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        #region Fields
        /// <summary>
        /// Represents a static logger instance initialized using NLog.
        /// </summary>
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        
        #endregion

        #region Actions

        /// <summary>
        /// use it to generate logs
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.Trace("SampleController: Get method called"); // Log Trace level
            _logger.Debug("SampleController: Get method called"); // Log Debug level
            _logger.Info("SampleController: Get method called");  // Log Info level
            _logger.Warn("SampleController: Get method called");  // Log Warn level
            _logger.Error("SampleController: Get method called"); // Log Error level
            _logger.Fatal("SampleController: Get method called"); // Log Fatal level

            return Ok();
        }

        /// <summary>
        /// dynamic file added 
        /// </summary>
        /// <param name="fileName">filename</param>
        /// <returns>response message</returns>
        [HttpPost("api/dynamic-file-add")]
        public IActionResult AddDynamicLog([FromBody] string fileName)
        {
            BLDynamicNlogConfig.AddDynamicFileTarget("dynamicFile", fileName);
            var logger = LogManager.GetLogger("dynamic");
            logger.Info("Added Dynamic file : {FileName}", fileName);
            return Ok("Added Dynamic file target ");
        }
        #endregion
    }
}
