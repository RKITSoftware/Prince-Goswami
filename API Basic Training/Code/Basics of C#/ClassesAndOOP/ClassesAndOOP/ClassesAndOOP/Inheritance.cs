using System;

namespace ClassesAndOOP
{
    class Inheritance
    {
        /// <summary>
        /// Runs the inheritance example.
        /// </summary>
        public static void InheritanceExample()
        {
            Console.WriteLine("\nInheritance Example:");

            Car objCar = new Car("Sedan", "Toyota");
            Bicycle objBicycle = new Bicycle("Mountain Bike", "Trek");

            DisplayVehicleInfo(objCar);
            DisplayVehicleInfo(objBicycle);

            Console.WriteLine();
        }

        /// <summary>
        /// Displays the vehicle information.
        /// </summary>
        /// <param name="vehicle">The vehicle object.</param>
        static void DisplayVehicleInfo(Vehicle vehicle)
        {
            Console.WriteLine($"{vehicle.Brand} {vehicle.Model}: {vehicle.GetVehicleInfo()}");
        }
    }

    /// <summary>
    /// Represents a vehicle.
    /// </summary>
    public class Vehicle
    {

        // Gets the model of the vehicle.
        public string Model { get; }

        // Gets the brand of the vehicle.
        public string Brand { get; }

        /// <summary>
        /// Initializes a new instance of the Vehicle class.
        /// </summary>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="brand">The brand of the vehicle.</param>
        public Vehicle(string model, string brand)
        {
            Model = model;
            Brand = brand;
        }

        /// <summary>
        /// Gets the vehicle information.
        /// </summary>
        /// <returns>The vehicle information.</returns>
        public virtual string GetVehicleInfo()
        {
            return "Generic vehicle info.";
        }
    }

    /// <summary>
    /// Represents a car. Inherited Vehicle
    /// </summary>
    public class Car : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the Car class.
        /// </summary>
        /// <param name="model">The model of the car.</param>
        /// <param name="brand">The brand of the car.</param>
        public Car(string model, string brand) : base(model, brand)
        {
        }

        /// <summary>
        /// Gets the vehicle information.
        /// </summary>
        /// <returns>The vehicle information.</returns>
        public override string GetVehicleInfo()
        {
            return "This is a car.";
        }
    }

    /// <summary>
    /// Represents a bicycle.
    /// </summary>
    public class Bicycle : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the Bicycle class.
        /// </summary>
        /// <param name="model">The model of the bicycle.</param>
        /// <param name="brand">The brand of the bicycle.</param>
        public Bicycle(string model, string brand) : base(model, brand)
        {
        }

        /// <summary>
        /// Gets the vehicle information.
        /// </summary>
        /// <returns>The vehicle information.</returns>
        public override string GetVehicleInfo()
        {
            return "This is a bicycle.";
        }
    }
}