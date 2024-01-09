using System;

namespace ScopeAndAccesibility
{
    public class Vehicle
    {
        #region Private field
        private float _fuelLevel;
        #endregion

        #region Protected field

        protected int Mileage;

        #endregion

        // Static field 
        private static int _totalVehicles;

        public Vehicle(float initialfuelLevel, int initialMileage)
        {
            _fuelLevel = initialfuelLevel;
            Mileage = initialMileage;
            _totalVehicles++;
        }

        public float GetfuelLevel()
        {
            return _fuelLevel;
        }

        public int GetMileage()
        {
            return Mileage;
        }

        // Static method 
        public static int GetTotalVehicles()
        {
            return _totalVehicles;
        }
    }

   

    public class Car : Vehicle
    {
        private static int _totalCars;

        public Car(float initialfuelLevel, int initialMileage, string model) : base(initialfuelLevel, initialMileage)
        {
            Model = model;
            _totalCars++;
        }

        public string Model { get; }

        // Static method 
        public static int GetTotalCars()
        {
            return _totalCars;
        }

        public void DisplayCarInfo()
        {
            Console.WriteLine($"Car Model: {Model}");
            Console.WriteLine($"Fuel Level: {GetfuelLevel()} liters"); // Accessing private method
            Console.WriteLine($"Mileage: {Mileage} miles"); // Accessing protected field
            Console.WriteLine($"Total Cars: {GetTotalCars()}"); // Accessing static method in the derived class
            Console.WriteLine($"Total Vehicles: {GetTotalVehicles()}"); // Accessing static method in the base class
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Simple Demo!");

            // Creating a car
            Car myCar = new Car(initialfuelLevel: 30.5f, initialMileage: 5000, model: "Sedan");

            // Creating another vehicle 
            Vehicle anotherVehicle = new Vehicle(initialfuelLevel: 40.0f, initialMileage: 3000);

            myCar.DisplayCarInfo();

            Console.ReadLine();
        }
    }
}









