using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exception_Handling.Models
{
    /// <summary>
    /// Logger class to handle logging at different levels.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Log information messages.
        /// </summary>
        public static void LogInformation(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }

        /// <summary>
        /// Log error messages along with exception details.
        /// </summary>
        public static void LogError(Exception ex, string message)
        {
            Console.WriteLine($"ERROR: {message}\n{ex}");
        }

        /// <summary>
        /// Log warning messages.
        /// </summary>
        public static void LogWarning(string message)
        {
            Console.WriteLine($"WARNING: {message}");
        }

        /// <summary>
        /// Log debug messages.
        /// </summary>
        public static void LogDebug(string message)
        {
            Console.WriteLine($"DEBUG: {message}");
        }
    }
 
}