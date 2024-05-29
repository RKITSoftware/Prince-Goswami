using System;

namespace DynamicTypeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example 1: Dynamic Variable
            dynamic dynamicVar = 10;
            Console.WriteLine($"Dynamic Variable: {dynamicVar}");

            dynamicVar = "Hello, Dynamic!";
            Console.WriteLine($"Dynamic Variable: {dynamicVar}");

            dynamicVar = DateTime.Now;
            Console.WriteLine($"Dynamic Variable: {dynamicVar}");

            Console.WriteLine();

            // Example 2: Dynamic Method Invocation
            dynamicCalculatorDemo();

            Console.ReadLine();
        }

        #region Dynamic Calculator Demo

        /// <summary>
        /// Demonstrates dynamic method invocation.
        /// </summary>
        static void dynamicCalculatorDemo()
        {
            // Dynamic method invocation example
            dynamic calculator = new DynamicCalculator();

            // The following method calls are resolved at runtime
            Console.WriteLine($"Addition: {calculator.Add(5, 10)}");
            Console.WriteLine($"Subtraction: {calculator.Subtract(15.5000, 7.54561)}");
            Console.WriteLine($"Multiplication: {calculator.Multiply(3, 4)}");
            Console.WriteLine($"Division: {calculator.Divide(20, 2)}");
        }

        #endregion
    }

    /// <summary>
    /// Sample dynamic calculator class
    /// </summary>
    class DynamicCalculator
    {
        public dynamic Add(dynamic a, dynamic b) => a + b;
        public dynamic Subtract(dynamic a, dynamic b) => a - b;
        public dynamic Multiply(dynamic a, dynamic b) => a * b;
        public dynamic Divide(dynamic a, dynamic b) => a / b;
    }
}
