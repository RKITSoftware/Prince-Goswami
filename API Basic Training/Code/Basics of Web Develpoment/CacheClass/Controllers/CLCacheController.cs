using CacheClass.Models;
using System;
using System.Web.Http;

namespace CacheClass.Controllers
{
    /// <summary>
    /// Controller for retrieving weather data.
    /// </summary>
    [RoutePrefix("api/weather")]
    public class CLCacheController : ApiController
    {
        /// <summary>
        /// Retrieves weather data for a specified city.
        /// </summary>
        /// <param name="city">The city for which weather data is requested.</param>
        /// <returns>The weather data for the specified city.</returns>
        [HttpGet]
        [Route("{city}")]
        public IHttpActionResult GetWeather(string city)
        {
            WeatherService weatherService = new WeatherService();
            BLWeatherData weatherData = weatherService.GetWeatherData(city);
            Console.Write(weatherService.Count);
            return Ok(weatherData);
        }
    }
}
