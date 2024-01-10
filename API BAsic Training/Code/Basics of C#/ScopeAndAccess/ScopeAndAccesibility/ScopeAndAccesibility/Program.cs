using System;

namespace ScopeAndAccesibility
{
    public class Vehicle
    {
        private float _fuelLevel;

        protected int mileage;

        private static int _totalVehicles;

        /// <summary>
        /// Constructor to initialize the Vehicle object
        /// </summary>
        /// <param name="initialFuelLevel">The initial fuel level of the vehicle.</param>
        /// <param name="initialMileage">The initial mileage of the vehicle.</param>
        public Vehicle(float initialFuelLevel, int initialMileage)
        {
            _fuelLevel = initialFuelLevel;
            mileage = initialMileage;
            _totalVehicles++;
        }

        /// <summary>
        /// Gets the fuel level of the vehicle.
        /// </summary>
        /// <returns>The fuel level of the vehicle.</returns>
        public float GetFuelLevel()
        {
            return _fuelLevel;
        }

        /// <summary>
        /// Gets the mileage of the vehicle.
        /// </summary>
        /// <returns>The mileage of the vehicle.</returns>
        public int GetMileage()
        {
            return mileage;
        }

        /// <summary>
        /// Gets the total number of vehicles created.
        /// </summary>
        /// <returns>The total number of vehicles created.</returns>
        public static int GetTotalVehicles()
        {
            return _totalVehicles;
        }
    }

    /// <summary>
    /// Derived class inheriting from Vehicle.
    /// </summary>
    public class Car : Vehicle
    {
        /// <summary>
        /// Static field to store the total number of cars created.
        /// </summary>
        private static int _totalCars;

        /// <summary>
        /// Constructor to initialize the Car object with the specified initial fuel level, mileage, and model.
        /// </summary>
        /// <param name="initialFuelLevel">The initial fuel level of the car.</param>
        /// <param name="initialMileage">The initial mileage of the car.</param>
        /// <param name="model">The model of the car.</param>
        public Car(float initialFuelLevel, int initialMileage, string model) : base(initialFuelLevel, initialMileage)
        {
            Model = model;
            _totalCars++;
        }

        /// <summary>
        /// Gets the model of the car.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the total number of cars created.
        /// </summary>
        /// <returns>The total number of cars created.</returns>
        public static int GetTotalCars()
        {
            return _totalCars;
        }

        /// <summary>
        /// Displays the information of the car
        /// </summary>
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