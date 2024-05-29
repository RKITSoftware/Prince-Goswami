using System;
using System.Web.Caching;
using CacheClass.Models;

/// <summary>
/// Service for retrieving weather data.
/// </summary>
public class WeatherService
{
    private const string WeatherApiUrl = "https://api.example.com/weather";
    private Cache _cache;

    /// <summary>
    /// Constructor for initializing the WeatherService.
    /// </summary>
    public WeatherService()
    {
        _cache = new Cache();
    }

    /// <summary>
    /// Retrieves weather data for a given city.
    /// </summary>
    /// <param name="city">The city for which weather data is requested.</param>
    /// <returns>The weather data for the specified city.</returns>
    public BLWeatherData GetWeatherData(string city)
    {
        string cacheKey = $"weather_{city.ToLower()}";

        // Try to get weather data from cache
        BLWeatherData cachedData = _cache.Get(cacheKey) as BLWeatherData;
        if (cachedData != null)
        {
            return cachedData;
        }

        // If not in cache, fetch the data from the external API
        BLWeatherData newData = FetchWeatherData();

        // Cache the data for a short duration (e.g., 15 seconds)
        _cache.Insert(cacheKey, newData, null, DateTime.Now.AddSeconds(15), Cache.NoSlidingExpiration);

        return newData;
    }

    /// <summary>
    /// Fetches weather data from an external API.
    /// </summary>
    /// <param name="city">The city for which weather data is fetched.</param>
    /// <returns>The fetched weather data.</returns>
    private BLWeatherData FetchWeatherData()
    {
        // Simulate fetching weather data from an external API
        // For simplicity, let's generate random data for temperature and description
        var random = new Random();
        double temperature = random.Next(-10, 30);
        string[] descriptions = { "Sunny", "Cloudy", "Rainy", "Snowy" };
        string description = descriptions[random.Next(0, descriptions.Length)];

        return new BLWeatherData { Temperature = temperature, Description = description };
    }

    #region CacheProperties

    // Properties of the Cache class can be accessed through the _cache object.

    /// <summary>
    /// Gets the number of items stored in the cache.
    /// </summary>
    public int Count => _cache.Count;

    #endregion
}
