using Caching.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Caching.Controllers
{

    [RoutePrefix("api/weather")]
    public class WeatherController : ApiController
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
