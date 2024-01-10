using System;

/// <summary>
/// This program demonstrates Exception Handling in C#.
/// </summary>
class ExceptionHandling
{
    /// <summary>
    /// The main entry point for the program.
    /// </summary>
    static void Main()
    {
        Console.WriteLine("Exception Handling Program:");

        try
        {
            // Prompt user for input
            Console.Write("Enter a number: ");
            string userInput = Console.ReadLine();

            // Attempt to convert user input to an integer
            int number = Convert.ToInt32(userInput);

            // Display the result
            Console.WriteLine($"You entered: {number}");
        }
        catch (FormatException)
        {
            // Handle the exception if user input cannot be converted to an integer
            Console.WriteLine("Invalid input! Please enter a valid number.");
        }
        catch (OverflowException)
        {
            // Handle the exception if the entered number is too large or too small
            Console.WriteLine("Number is too large or too small. Please enter a valid number.");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // This block is optional, and code here will execute whether an exception occurs or not
            Console.WriteLine("Program execution completed.");
        }

        Console.ReadLine();
    }
}