using System;

namespace Methods
{
    internal class Program
    {
        /// <summary>
        /// Main method of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Enter two numbers :");
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());

            NumberOperation numberOperation = new NumberOperation();
            Console.WriteLine("Maximum Number is " + numberOperation.FindMax(a, b));

        }
    }

    class NumberOperation
    {
        /// <summary>
        /// Finds the maximum of two numbers.
        /// </summary>
        /// <param name="num1">The first number.</param>
        /// <param name="num2">The second number.</param>
        /// <returns>The maximum of the two numbers.</returns>
        public int FindMax(int num1, int num2)
        {
            int result;

            if (num1 > num2)
                result = num1;
            else
                result = num2;
            return result;
        }

    }
}