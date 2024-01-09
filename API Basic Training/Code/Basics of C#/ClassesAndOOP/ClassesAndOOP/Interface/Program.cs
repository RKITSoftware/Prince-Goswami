using System;

namespace InterfaceDemo
{
    // Define an interface
    public interface IShape
    {
        /// <summary>
        /// Calculates the area of the shape.
        /// </summary>
        /// <returns>The area of the shape.</returns>
        double CalculateArea();

        /// <summary>
        /// Calculates the perimeter of the shape.
        /// </summary>
        /// <returns>The perimeter of the shape.</returns>
        double CalculatePerimeter();
    }

    // Implement the interface in a class
    public class Circle : IShape
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public double CalculateArea()
        {
            return Math.PI * radius * radius;
        }

        public double CalculatePerimeter()
        {
            return 2 * Math.PI * radius;
        }
    }

    // Implement the interface in another class
    public class Rectangle : IShape
    {
        private double length;
        private double width;

        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public double CalculateArea()
        {
            return length * width;
        }

        public double CalculatePerimeter()
        {
            return 2 * (length + width);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create objects of Circle and Rectangle classes
            Circle circle = new Circle(5);
            Rectangle rectangle = new Rectangle(4, 6);

            // Call the methods of the interface
            Console.WriteLine("Circle Area: " + circle.CalculateArea());
            Console.WriteLine("Circle Perimeter: " + circle.CalculatePerimeter());

            Console.WriteLine("Rectangle Area: " + rectangle.CalculateArea());
            Console.WriteLine("Rectangle Perimeter: " + rectangle.CalculatePerimeter());

            Console.ReadLine();
        }
    }
}