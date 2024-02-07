// IWeatherService.cs
using Caching.Models;

public interface IWeatherService
{
    BLWeatherData GetWeatherData(string city);
}
