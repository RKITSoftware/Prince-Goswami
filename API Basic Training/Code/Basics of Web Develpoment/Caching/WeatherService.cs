// WeatherService.cs
using System;
using System.Net.Http;
using Caching.Models;
using Microsoft.Extensions.Caching.Memory;

public class WeatherService : IWeatherService
{
    private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
    private static readonly HttpClient HttpClient = new HttpClient();
    private const string WeatherApiUrl = "https://api.example.com/weather";

    public BLWeatherData GetWeatherData(string city)
    {
        string cacheKey = $"weather_{city.ToLower()}";

        //// Try to get weather data from cache
        //if (Cache.TryGetValue(cacheKey, out BLWeatherData cachedData))
        //{
        //    return cachedData;
        //}

        // If not in cache, fetch the data from the external API
        BLWeatherData newData = FetchWeatherData(city);

        //// Cache the data for a short duration (e.g., 5 minutes)
        //var cacheEntryOptions = new MemoryCacheEntryOptions
        //{
        //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        //};

        //Cache.Set(cacheKey, newData, cacheEntryOptions);

        return newData;
    }

    private BLWeatherData FetchWeatherData(string city)
    {
        // Simulate fetching weather data from an external API
        // For simplicity, let's generate random data for temperature and description
        var random = new Random();
        double temperature = random.Next(-10, 30);
        string[] descriptions = { "Sunny", "Cloudy", "Rainy", "Snowy" };
        string description = descriptions[random.Next(0, descriptions.Length)];

        return new BLWeatherData { Temperature = temperature, Description = description };
    }
}
