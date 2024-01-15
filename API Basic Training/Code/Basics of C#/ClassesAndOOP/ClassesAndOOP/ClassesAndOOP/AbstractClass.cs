using System;

namespace ClassesAndOOP
{
    #region Abstraction
    // Abstraction
    public abstract class Shape
    {
        public abstract double CalculateArea();

        // Concrete method
        public void DisplayDetails()
        {
            Console.WriteLine($"This is a {GetType().Name}.");
        }
    }
    #endregion
}