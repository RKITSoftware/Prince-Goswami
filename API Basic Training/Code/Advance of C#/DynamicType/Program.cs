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

        static void dynamicCalculatorDemo()
        {
            // Dynamic method invocation example
            dynamic calculator = new Calculator();

            // The following method calls are resolved at runtime
            Console.WriteLine($"Addition: {calculator.Add(5, 10)}");
            Console.WriteLine($"Subtraction: {calculator.Subtract(15, 7)}");
            Console.WriteLine($"Multiplication: {calculator.Multiply(3, 4)}");
            Console.WriteLine($"Division: {calculator.Divide(20, 2)}");
        }
    }

    // Sample class for dynamic method invocation
    class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        public int Multiply(int a, int b) => a * b;
        public int Divide(int a, int b) => a / b;
    }
}
