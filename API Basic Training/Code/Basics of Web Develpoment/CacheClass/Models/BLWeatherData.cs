using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheClass.Models
{
    /// <summary>
    /// Represents weather data including temperature and description.
    /// </summary>
    public class BLWeatherData
    {
        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Gets or sets the weather description.
        /// </summary>
        public string Description { get; set; }
    }
}
