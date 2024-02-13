using System;
using System.Linq;

// Define an extension method class
public static class StringExtensions
{
    // Extension method using a lambda expression
    public static string ToUpper(this string input, Func<char, char> processor)
    {
        // Use LINQ to apply the lambda expression to each character in the string
        char[] processedChars = input.Select(processor).ToArray();
        return new string(processedChars);
    }
}

class Program
{
    static void Main()
    {
        string original = "Hello, Extension Methods!";

        // Using the extension method with a lambda expression
        string result = original.ToUpper(c => char.IsLetter(c) ? char.ToUpper(c) : c);

        Console.WriteLine($"Original: {original}");
        Console.WriteLine($"Processed: {result}");

        Console.ReadLine();
    }
}
