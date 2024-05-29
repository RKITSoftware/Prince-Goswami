using System;

namespace Debugging
{
    /// <summary>
    /// Demonstrates the usage of data inspection in debugging.
    /// </summary>
    public static class DataInspector
    {
        /// <summary>
        /// Runs the demonstration of data inspection.
        /// </summary>
        public static void Run()
        {
            string[] names = { "Alice", "Bob", "Charlie", "David", "Eve" };
            DisplayNames(names);
        }

        /// <summary>
        /// Displays the names in the specified array.
        /// </summary>
        /// <param name="names">An array of names to display.</param>
        static void DisplayNames(string[] names)
        {
            foreach (string name in names)
            {
                Console.WriteLine($"Name: {name}");
            }
        }
    }
}
