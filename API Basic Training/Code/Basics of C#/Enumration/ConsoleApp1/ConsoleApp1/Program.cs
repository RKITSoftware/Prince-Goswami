using System;

// Enumeration to represent days of the week
public enum DayOfWeek
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

class Enumeration
{
    static void Main()
    {
        Console.WriteLine("Enumerations Program:");

        // Using the DayOfWeek enumeration
        DayOfWeek today = DayOfWeek.Wednesday;

        Console.WriteLine($"Today is {today}");

        // Switch statement based on the DayOfWeek enum
        switch (today)
        {
            case DayOfWeek.Monday:
            case DayOfWeek.Tuesday:
            case DayOfWeek.Wednesday:
            case DayOfWeek.Thursday:
            case DayOfWeek.Friday:
                Console.WriteLine("It's a weekday. Keep working!");
                break;
            case DayOfWeek.Saturday:
            case DayOfWeek.Sunday:
                Console.WriteLine("It's the weekend. Time to relax!");
                break;
            default:
                Console.WriteLine("Invalid day");
                break;
        }

        Console.ReadLine();
    }
}
