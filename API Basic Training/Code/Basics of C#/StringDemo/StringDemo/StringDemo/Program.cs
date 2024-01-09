using System;

class StringOperations
{
    /// <summary>
    /// Demonstrates various string operations such as concatenation, length, substring, case conversion, index of, replace, split, and string comparison.
    /// </summary>
    static void Main()
    {
        // Creating strings
        string str1 = "Hello, ";
        string str2 = "world!";

        // Concatenation
        string str = str1 + str2;
        Console.WriteLine("Concatenation Result: " + str);

        // Length
        int length = str.Length;
        Console.WriteLine("Length of the string: " + length);

        // Substring
        string substring = str.Substring(0, 5);
        Console.WriteLine("Substring (0, 5): " + substring);

        // ToUpper and ToLower
        string upperCase = str.ToUpper();
        string lowerCase = str.ToLower();
        Console.WriteLine("Uppercase: " + upperCase);
        Console.WriteLine("Lowercase: " + lowerCase);

        // IndexOf
        int indexOfWorld = str.IndexOf("world");
        Console.WriteLine("Index of 'world': " + indexOfWorld);

        // Replace
        string replacedString = str.Replace("world", "C#");
        Console.WriteLine("After Replacement: " + replacedString);

        // Split
        string sentence = "RKIT software PVT LTD Rajkot";
        string[] words = sentence.Split(' ');
        Console.WriteLine("Words in the sentence:");
        foreach (var word in words)
        {
            Console.WriteLine(word);
        }

        // Comparing strings
        string str3 = "Hello, world!";
        bool areEqual = str.Equals(str3);
        Console.WriteLine("Are str2 and str3 equal? " + areEqual);
    }
}