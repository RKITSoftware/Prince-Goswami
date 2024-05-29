using System;

namespace Debugging
{
    /// <summary>
    /// Demonstrates the usage of conditional breakpoints in debugging.
    /// </summary>
    public static class ConditionalBreakpoint
    {
        /// <summary>
        /// Runs the demonstration of conditional breakpoints.
        /// </summary>
        public static void Run()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            int sum = CalculateSum(numbers, true); // Call CalculateSum method with debug mode enabled
            Console.WriteLine($"Sum of numbers: {sum}");
        }

        /// <summary>
        /// Calculates the sum of numbers and optionally prints debug messages.
        /// </summary>
        /// <param name="numbers">An array of integers.</param>
        /// <param name="debugMode">A boolean flag indicating whether debug messages should be printed.</param>
        /// <returns>The sum of numbers.</returns>
        static int CalculateSum(int[] numbers, bool debugMode)
        {
            int sum = 0;
            foreach (int num in numbers)
            {
                if (debugMode)
                {
                    Console.WriteLine($"Debugging: Adding {num} to sum");
                }
                sum += num;
                
            }
            return sum;
        }
    }
}
