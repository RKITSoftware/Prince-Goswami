using System;

namespace InterfaceDemo
{
    /// <summary>
    /// Represents the IShape interface, which defines methods for calculating the area and perimeter of a shape.
    /// </summary>
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

    /// <summary>
    /// Represents a circle, implementing the IShape interface.
    /// </summary>
    public class Circle : IShape
    {
        private double radius;

        /// <summary>
        /// Initializes a new instance of the Circle class with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(double radius)
        {
            this.radius = radius;
        }

        /// <summary>
        /// Calculates the area of the circle.
        /// </summary>
        /// <returns>The area of the circle.</returns>
        public double CalculateArea()
        {
            return Math.PI * radius * radius;
        }

        /// <summary>
        /// Calculates the perimeter of the circle.
        /// </summary>
        /// <returns>The perimeter of the circle.</returns>
        public double CalculatePerimeter()
        {
            return 2 * Math.PI * radius;
        }
    }

    /// <summary>
    /// Represents a rectangle, implementing the IShape interface.
    /// </summary>
    public class Rectangle : IShape
    {
        private double length;
        private double width;

        /// <summary>
        /// Initializes a new instance of the Rectangle class with the specified length and width.
        /// </summary>
        /// <param name="length">The length of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        /// <summary>
        /// Calculates the area of the rectangle.
        /// </summary>
        /// <returns>The area of the rectangle.</returns>
        public double CalculateArea()
        {
            return length * width;
        }

        /// <summary>
        /// Calculates the perimeter of the rectangle.
        /// </summary>
        /// <returns>The perimeter of the rectangle.</returns>
        public double CalculatePerimeter()
        {
            return 2 * (length + width);
        }
    }

    /// <summary>
    /// Contains the entry point of the program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
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