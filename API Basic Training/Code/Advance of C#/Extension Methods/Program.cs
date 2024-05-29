using System;

// Define an extension method class
public static class StringExtensions
{
    /// <summary>
    /// Extension method for replacing spaces with underscores
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string Transform(this string input)
    {
        return input.Replace(' ', '_');
    }
}

class Program
{
    static void Main()
    {
        string original = "The quick brown fox jumps over the lazy dog.";

        // Using the extension method to replace spaces with underscores
        string result = original.Transform();

        Console.WriteLine($"Original: {original}");
        Console.WriteLine($"Modified: {result}");
    }
}
