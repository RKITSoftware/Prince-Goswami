using System;
using ClassesAndOOP;

namespace ClassesAndOOP
{
    public class TypesOfClass
    {
        #region RegularClassExample        
        // Regular class example
        public static void RegularClassExample()
        {
            Console.WriteLine("Regular Class Example:");

            Box b1 = new Box(10.00, 20.00, 30.00);
            Console.WriteLine("length : " + b1.getLength());
            Console.WriteLine("breath : " + b1.getBreath());
            Console.WriteLine("width : " + b1.getWidth());
            double area = b1.getArea();
            Console.WriteLine("Area of box is : " + area);
        }
        #endregion

        #region AbstractClassExample
        // Abstract class example
        public static void AbstractClassExample()
        {
            Console.WriteLine("\nAbstract Class Example:");

            // Creating an instance of the derived class
            Circle objCircle = new Circle(5);

            // Accessing properties and invoking methods
            Console.WriteLine($"Radius: {objCircle.Radius}");
            Console.WriteLine($"Area: {objCircle.CalculateArea()}");

            Console.WriteLine();
        }
        #endregion

        #region StaticClassExample
        // Static class example
        public static void StaticClassExample()
        {
            Console.WriteLine("\nStatic Class Example:");

            // Accessing static members without creating an instance
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

            // Creating an instance of the sealed class
            FinalClass objFinal = new FinalClass();

            // Sealed classes do not support inheritance
            // DerivedClass objDerived = new DerivedClass();

            Console.WriteLine("Sealed class object created.");

            Console.WriteLine();
        }
        #endregion

    }

    #region Regular Class
    class Box
    {
        private double length, breath, width;

        public Box(double length, double breath, double width)
        {
            this.length = length;
            this.breath = breath;
            this.width = width;
        }

        public double getArea()
        {
            return length * breath * width;
        }

        public double getLength() => length;
        public double getWidth() => width;
        public double getBreath() => breath;
    }
#endregion

    #region Abstract Class
    public class Circle : Shape
    {
        // Property
        public double Radius { get; set; }

        // Constructor
        public Circle(double radius)
        {
            Radius = radius;
        }

        // Implementation of abstract method
        public override double CalculateArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
    }
    #endregion

    #region Static Class
    // Static class
    public static class MathUtility
    {
        // Static methods
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

    #region Sealed Class
    // Sealed class
    public sealed class FinalClass
    {
        // Sealed class implementation
    }

    // public class DerivedClass : FinalClass { }
    #endregion
}