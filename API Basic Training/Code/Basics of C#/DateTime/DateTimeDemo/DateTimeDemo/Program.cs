using System;

namespace DateTimeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Current DateTime
            // Get the current date and time
            DateTime currentDateTime = DateTime.Now;
            Console.WriteLine("Current Date and Time: " + currentDateTime);

            // Get the current date
            DateTime currentDate = DateTime.Today;
            Console.WriteLine("Current Date: " + currentDate);

            // Get the current time
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            Console.WriteLine("Current Time: " + currentTime);

            // Get the year, month, day, hour, minute, and second
            int year = currentDateTime.Year;
            int month = currentDateTime.Month;
            int day = currentDateTime.Day;
            int hour = currentDateTime.Hour;
            int minute = currentDateTime.Minute;
            int second = currentDateTime.Second;
            Console.WriteLine("Year: " + year);
            Console.WriteLine("Month: " + month);
            Console.WriteLine("Day: " + day);
            Console.WriteLine("Hour: " + hour);
            Console.WriteLine("Minute: " + minute);
            Console.WriteLine("Second: " + second);

            // Add days, hours, minutes, and seconds to a date
            DateTime futureDateTime = currentDateTime.AddDays(5).AddHours(2).AddMinutes(30).AddSeconds(45);
            Console.WriteLine("Future Date and Time: " + futureDateTime);

            // Subtract days, hours, minutes, and seconds from a date
            DateTime pastDateTime = currentDateTime.AddDays(-5).AddHours(-2).AddMinutes(-30).AddSeconds(-45);
            Console.WriteLine("Past Date and Time: " + pastDateTime);
            #endregion

            #region Helping Methods
            // Compare two dates
            DateTime date1 = new DateTime(2022, 1, 1);
            DateTime date2 = new DateTime(2022, 12, 31);
            int result = DateTime.Compare(date1, date2);
            Console.WriteLine("Comparison Result: " + result);

            // Check if a year is a leap year
            int yearToCheck = 2024;
            bool isLeapYear = DateTime.IsLeapYear(yearToCheck);
            Console.WriteLine(yearToCheck + " is a leap year: " + isLeapYear);

            // Format a date to a string
            string formattedDate = currentDateTime.ToString("MM/dd/yyyy");
            Console.WriteLine("Formatted Date: " + formattedDate);

            // Parse a string to a date
            string dateString = "12/31/2022";
            DateTime parsedDate = DateTime.Parse(dateString);
            Console.WriteLine("Parsed Date: " + parsedDate);

            // Get the number of days in a month
            int daysInMonth = DateTime.DaysInMonth(year, month);
            Console.WriteLine("Days in Month: " + daysInMonth);

            // Get the day of the week
            DayOfWeek dayOfWeek = currentDateTime.DayOfWeek;
            Console.WriteLine("Day of the Week: " + dayOfWeek);

            // Get the number of days between two dates
            DateTime date3 = new DateTime(2022, 1, 1);
            DateTime date4 = new DateTime(2022, 12, 31);
            TimeSpan difference = date4 - date3;
            int daysDifference = difference.Days;
            Console.WriteLine("Days Difference: " + daysDifference);

            // Get the number of hours, minutes, and seconds between two dates
            int hoursDifference = difference.Hours;
            int minutesDifference = difference.Minutes;
            int secondsDifference = difference.Seconds;
            Console.WriteLine("Hours Difference: " + hoursDifference);
            Console.WriteLine("Minutes Difference: " + minutesDifference);
            Console.WriteLine("Seconds Difference: " + secondsDifference);

            // Check if a date is in daylight saving time
            bool isDaylightSavingTime = currentDateTime.IsDaylightSavingTime();
            Console.WriteLine("Is Daylight Saving Time: " + isDaylightSavingTime);

            // Get the date and time in a specific time zone
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            DateTime dateTimeInTimeZone = TimeZoneInfo.ConvertTime(currentDateTime, timeZone);
            Console.WriteLine("Date and Time in Pacific Standard Time: " + dateTimeInTimeZone);

            // Get the current date and time in UTC
            DateTime utcDateTime = DateTime.UtcNow;
            Console.WriteLine("Current Date and Time (UTC): " + utcDateTime);

            // Get the date and time in a specific format
            string customFormat = "dddd, MMMM d, yyyy HH:mm:ss";
            string formattedDateTime = currentDateTime.ToString(customFormat);
            Console.WriteLine("Formatted Date and Time: " + formattedDateTime);
            #endregion

            // Wait for user input before closing the console window
            Console.ReadLine();
        }
    }
}