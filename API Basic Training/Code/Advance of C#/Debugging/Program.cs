using System;

namespace Debugging
{
    /// <summary>
    /// Entry point of the application. Orchestrates the execution of various debugging demos.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main method where the execution starts.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Run the debugging demos
            Debugging.Run(); // Basic debugging functionalities
            DataInspector.Run(); // Data inspection in debugging
            ConditionalBreakpoint.Run(); // Demonstrating conditional breakpoints
            ConditionalCompilation.Run(); // Demonstrating conditional compilation
            Console.ReadLine();
        }
    }
}
