using System;

namespace ScopeAndAccesibility
{
    public class Vehicle
    {
        // Private field 
        private float fuelLevel;

        // Protected field
        protected int mileage;

        // Static field 
        private static int totalVehicles;

        public Vehicle(float initialFuelLevel, int initialMileage)
        {
            fuelLevel = initialFuelLevel;
            mileage = initialMileage;
            totalVehicles++;
        }

        public float GetFuelLevel()
        {
            return fuelLevel;
        }

        public int GetMileage()
        {
            return mileage;
        }

        // Static method 
        public static int GetTotalVehicles()
        {
            return totalVehicles;
        }
    }

    // Derived class inheriting from Vehicle
    public class Car : Vehicle
    {
        private static int totalCars;

        public Car(float initialFuelLevel, int initialMileage, string model) : base(initialFuelLevel, initialMileage)
        {
            Model = model;
            totalCars++;
        }

        public string Model { get; }

        // Static method 
        public static int GetTotalCars()
        {
            return totalCars;
        }

        public void DisplayCarInfo()
        {
            Console.WriteLine($"Car Model: {Model}");
            Console.WriteLine($"Fuel Level: {GetFuelLevel()} liters"); // Accessing private method
            Console.WriteLine($"Mileage: {mileage} miles"); // Accessing protected field
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
            Car myCar = new Car(initialFuelLevel: 30.5f, initialMileage: 5000, model: "Sedan");

            // Creating another vehicle 
            Vehicle anotherVehicle = new Vehicle(initialFuelLevel: 40.0f, initialMileage: 3000);

            myCar.DisplayCarInfo();

            Console.ReadLine();
        }
    }
}
