using CacheClass.Models;
using System.Web.Http;

namespace CacheClass.Controllers
{
    [RoutePrefix("api/weather")]
    public class CLCacheController : ApiController
    {
        [HttpGet]
        [Route("{city}")]
        public IHttpActionResult GetWeather(string city)
        {
            WeatherService weatherService = new WeatherService();
            BLWeatherData weatherData = weatherService.GetWeatherData(city);
            return Ok(weatherData);
        }
    }
}
