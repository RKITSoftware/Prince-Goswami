= zusing System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassesAndOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Class
            Box b1 = new Box(10.00, 20.00, 30.00);
            Console.WriteLine("length : " + b1.getLength());
            Console.WriteLine("breath : " + b1.getBreath());
            Console.WriteLine("width : " + b1.getWidth());
            double area = b1.getArea();
            Console.WriteLine("Area of box is : " + area);
            #endregion

            #region OOP
            // Encapsulation
            Car myCar = new Car("Toyota", 2022);
            myCar.Start();

            // Inheritance
            ElectricCar myElectricCar = new ElectricCar("Tesla", 2023);
            myElectricCar.Start();
            myElectricCar.Charge();

            // Polymorphism
            IDriveable gasCar = new GasCar("Ford", 2021);
            gasCar.Drive();

            // Abstraction
            Circle myCircle = new Circle(5);
            Console.WriteLine($"Area of the circle: {myCircle.CalculateArea()} square units");

            #endregion
        }
    }

    #region class
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

    #region Encapsulation
    // Encapsulation
    public class Car
    {
        private string model;
        private int year;

        // Properties using encapsulation
        public string Model
        {
            get { return model; }
            set
            {
                if (value != null)
                {
                    model = value;
                }
                else
                {
                    Console.WriteLine("Invlaid Model");
                }
            }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        // Constructor
        public Car(string model, int year)
        {
            this.model = model;
            this.year = year;
        }

        // Method using encapsulation
        public void Start()
        {
            Console.WriteLine($"The {year} {model} is starting.");
        }
    }
    #endregion

    #region Inheritance
    // Inheritance
    public class ElectricCar : Car
    {
        public ElectricCar(string model, int year) : base(model, year)
        {

        }

        // Additional method specific to ElectricCar
        public void Charge()
        {
            Console.WriteLine($"Charging the {Year} {Model}.");
        }
    }
    #endregion

    #region Polymorphism
    // Polymorphism
    public interface IDriveable
    {
        void Drive();
    }

    public class GasCar : Car, IDriveable
    {
        public GasCar(string model, int year) : base(model, year)
        {
        }

        // Implementing interface method
        public void Drive()
        {
            Console.WriteLine($"Driving the {Year} {Model} using gas.");
        }
    }
    #endregion

    #region Abstraction
    // Abstraction
    public abstract class Shape
    {
        public abstract double CalculateArea();
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        // Implementing abstract method
        public override double CalculateArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
    }
    #endregion`
}
