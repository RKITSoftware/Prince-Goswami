using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// This program demonstrates File Opereations in C#.
/// </summary>
class Program
{
    /// <summary>
    /// The main entry point for the program.
    /// </summary>
    static void Main()
    {


        string s = "1,2,3";
        List<string> list = new List<string>();
        list = s.Split(new char[] { ',' });
        Console.Write(Convert.ToString(list));


        Console.WriteLine("Simple Note Taker");

        // Specify the file path for notes
        string filePath = "my_notes.txt";

        // Menu for user interaction
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Write a new note");
            Console.WriteLine("2. Read all notes");
            Console.WriteLine("3. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNoteToFile(filePath);
                    break;
                case "2":
                    ReadAllNotes(filePath);
                    break;
                case "3":
                    Console.WriteLine("Exiting the Simple Note Taker. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }

    #region Note Writing

    /// <summary>
    /// Writes a new note to the specified file.
    /// </summary>
    /// <param name="filePath">The path to the notes file.</param>
    static void WriteNoteToFile(string filePath)
    {
        try
        {
            Console.WriteLine("Write your note (press Enter to finish):");

            // Open or create the notes file for writing
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                // Read user input and write to the notes file
                string input;
                while (!string.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    writer.WriteLine(input);
                }
            }

            Console.WriteLine("Your note has been saved!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to the notes file: {ex.Message}");
        }
    }

    #endregion

    #region Note Reading

    /// <summary>
    /// Reads all notes from the specified file and displays them.
    /// </summary>
    /// <param name="filePath">The path to the notes file.</param>
    static void ReadAllNotes(string filePath)
    {
        try
        {
            // Check if the notes file exists before reading
            if (File.Exists(filePath))
            {
                // Open the notes file for reading
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Read and display all notes
                    Console.WriteLine("\n--- All Notes ---");
                    Console.WriteLine(reader.ReadToEnd());
                    Console.WriteLine("-----------------");
                }
            }
            else
            {
                Console.WriteLine("No notes found. Start writing your first note!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading from the notes file: {ex.Message}");
        }
    }

    #endregion
}
