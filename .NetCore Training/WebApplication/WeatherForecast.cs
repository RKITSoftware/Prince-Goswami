namespace WebApplication
{
    /// <summary>
    /// Represents a weather forecast for a specific date.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Gets or sets the date of the weather forecast.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in Celsius for the forecasted date.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets the temperature in Fahrenheit for the forecasted date.
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Gets or sets a summary describing the weather conditions for the forecasted date.
        /// </summary>
        public string? Summary { get; set; }
    }
}
