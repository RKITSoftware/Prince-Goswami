
using System;
using ClassesAndOOP;

namespace ClassesAndOOP
{
    #region TypesOfClass
    /// <sumary>
    /// Class to demonstrate different types of classes
    ///</summary>
    public class TypesOfClass
    {
        #region RegularClassExample

        // Regular class example
        public static void RegularClassExample()
        {
            Console.WriteLine("Regular Class Example:");

            // Creating an instance of the Box class
            Box b1 = new Box(10.00, 20.00, 30.00);

            // Accessing properties and methods of the Box class
            Console.WriteLine("length: " + b1.getLength());
            Console.WriteLine("breath: " + b1.getBreath());
            Console.WriteLine("width: " + b1.getWidth());
            double area = b1.getArea();
            Console.WriteLine("Area of box is: " + area);
        }

        #endregion

        #region AbstractClassExample

        // Abstract class example
        public static void AbstractClassExample()
        {
            Console.WriteLine("\nAbstract Class Example:");

            // Creating an instance of the derived class Circle
            Circle objCircle = new Circle(5);

            // Accessing properties and invoking methods of the Circle class
            Console.WriteLine($"Radius: {objCircle.Radius}");
            Console.WriteLine($"Area: {objCircle.CalculateArea()}");
            objCircle.DisplayDetails();
            Console.WriteLine();
        }

        #endregion

        #region StaticClassExample

        // Static class example
        public static void StaticClassExample()
        {
            Console.WriteLine("\nStatic Class Example:");

            // Accessing static members of the MathUtility class without creating an instance
            int sumResult = MathUtility.Add(10, 5);
            double sqrtResult = MathUtility.SquareRoot(25);

            Console.WriteLine($"Sum: {sumResult}, Square Root: {sqrtResult}");

            Console.WriteLine();
        }

        #endregion

        #region SealedClassExample

        // Sealed class example
        public static void SealedClassExample()
        {
            Console.WriteLine("\nSealed Class Example:");

            // Creating an instance of the sealed class FinalClass
            FinalClass objFinal = new FinalClass();

            // Sealed classes do not support inheritance
            // DerivedClass objDerived = new DerivedClass();

            Console.WriteLine("Sealed class object created.");

            Console.WriteLine();
        }

        #endregion

    }

    #endregion

    #region BoxClass

    // Regular class named Box
    class Box
    {
        private double length, breath, width;

        // Constructor for the Box class
        public Box(double length, double breath, double width)
        {
            this.length = length;
            this.breath = breath;
            this.width = width;
        }

        // Method to calculate the area of the box
        public double getArea()
        {
            return length * breath * width;
        }

        // Getter methods for the dimensions of the box
        public double getLength() => length;
        public double getWidth() => width;
        public double getBreath() => breath;
    }

    #endregion

    #region CircleClass

    // Derived class Circle from the abstract class Shape
    public class Circle : Shape
    {
        // Property for the radius of the circle
        public double Radius { get; set; }

        // Constructor for the Circle class
        public Circle(double radius)
        {
            Radius = radius;
        }

        // Implementation of the abstract method from the Shape class
        public override double CalculateArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
       
    }

    #endregion

    #region MathUtilityClass

    // Static class named MathUtility
    public static class MathUtility
    {
        // Static methods for mathematical operations
        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static double SquareRoot(double number)
        {
            return Math.Sqrt(number);
        }
    }

    #endregion

    #region FinalClass

    // Sealed class named FinalClass
    public sealed class FinalClass
    {
        // Implementation of a sealed class
    }

    #endregion

}
