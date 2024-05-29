using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /// <summary>
    /// Entry point of the program.
    /// </summary>
    static void Main()
    {
        // Define a list of integers
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // Use a lambda expression to filter the list for even numbers
        IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);

        // Print the even numbers
        Console.WriteLine("Even numbers:");
        foreach (var num in evenNumbers)
        {
            Console.WriteLine(num);
        }

        // Use a lambda expression to create a list of squares of the numbers
        List<int> squares = numbers.Select(n => n * n).ToList();

        // Print the squares
        Console.WriteLine("\nSquares of numbers:");
        foreach (var square in squares)
        {
            Console.WriteLine(square);
        }

        // Use a lambda expression to calculate the sum of all numbers
        int sum = numbers.Sum(n => n);

        // Print the sum
        Console.WriteLine($"\nSum of numbers: {sum}");

        // Use a lambda expression to find the maximum number in the list
        int max = numbers.Max(n => n);

        // Print the maximum number
        Console.WriteLine($"\nMaximum number: {max}");
    }
}
