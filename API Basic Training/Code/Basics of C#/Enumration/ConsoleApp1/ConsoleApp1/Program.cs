using System;

/// <summary>
/// Enumeration to represent days of the week
/// </summary>
public enum enumDayOfWeek
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

public enum enumMulti
{
    Saturday,
    0123,
    {1,2,3,4}
}
class Enumeration
{
    /// <summary>
    /// Main entry point of the program
    /// </summary>
    static void Main()
    {
        Console.WriteLine("Enumerations Program:");
        enumMulti multi = 2;
        Console.WriteLine("Enumerations :" + multi);

        // Using the DayOfWeek enumeration
        DayOfWeek today = DayOfWeek.Friday;

        // Display today's value from the DayOfWeek enum
        Console.WriteLine($"Today is {today}");

        // Switch statement based on the DayOfWeek enum
        switch (today)
        {
            // For weekdays (Monday to Friday), display a message about working
            case DayOfWeek.Monday:
            case DayOfWeek.Tuesday:
            case DayOfWeek.Wednesday:
            case DayOfWeek.Thursday:
            case DayOfWeek.Friday:
                Console.WriteLine("It's a weekday. Keep working!");
                break;
            // For weekends (Saturday and Sunday), display a message about relaxing
            case DayOfWeek.Saturday:
            case DayOfWeek.Sunday:
                Console.WriteLine("It's the weekend. Time to relax!");
                break;
            // Displayed when the day doesn't match any valid enum value
            default:
                Console.WriteLine("Invalid day");
                break;
        }

        // Wait for user input before closing the console window
        Console.ReadLine();
    }
}
