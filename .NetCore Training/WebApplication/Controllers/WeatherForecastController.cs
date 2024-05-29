using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Controller for fetching weather forecasts.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        #region Private Fields

        // Array of weather summaries to randomly select from.
        private static readonly string[] _Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Retrieves a list of weather forecasts.
        /// </summary>
        /// <returns>An IEnumerable of WeatherForecast objects representing the weather forecast.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        [AllowAnonymous]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _Summaries[Random.Shared.Next(_Summaries.Length)]
            })
            .ToArray();
        }

        #endregion
    }
}
