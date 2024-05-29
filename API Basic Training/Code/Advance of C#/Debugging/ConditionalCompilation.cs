using System;

namespace Debugging
{
    /// <summary>
    /// Demonstrates the usage of conditional compilation in debugging.
    /// </summary>
    public static class ConditionalCompilation
    {
        /// <summary>
        /// Runs the demonstration of conditional compilation.
        /// </summary>
        public static void Run()
        {
            #if DEBUG
                Console.WriteLine("Debug mode is enabled.");
            #else
                Console.WriteLine("Debug mode is disabled.");
            #endif
        }
    }
}
