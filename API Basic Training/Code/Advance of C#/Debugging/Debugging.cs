using System;

namespace Debugging
{
    /// <summary>
    /// Demonstrates basic debugging functionalities like setting breakpoints and stepping through code.
    /// </summary>
    public static class Debugging
    {
        /// <summary>
        /// Runs the demonstration of basic debugging functionalities.
        /// </summary>
        public static void Run()
        {
            int a = 7;
            int b = 20;
            int result = AddNumbers(a, b);
            Console.WriteLine($"Result: {result}");
        }

        /// <summary>
        /// Adds two numbers and returns the sum.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The sum of the two numbers.</returns>
        static int AddNumbers(int x, int y)
        {
            int sum = x + y;
            return sum;
        }
    }
}
