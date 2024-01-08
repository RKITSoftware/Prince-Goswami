using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesAndOOP
{
    using System;

    /// <summary>
    /// Main program class that demonstrates inheritance in C# with vehicles.
    /// </summary>
    class Inheritance
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        public static void inheritanceRun()
        {
            Console.WriteLine("\nInheritance Example:");

            // Creating instances of different vehicles
            Car objCar = new Car("Sedan", "Toyota");
            Bicycle objBicycle = new Bicycle("Mountain Bike", "Trek");

            // Displaying information about different vehicles using inheritance
            DisplayVehicleInfo(objCar);
            DisplayVehicleInfo(objBicycle);

            Console.WriteLine();
        }

        /// <summary>
        /// Displays information about a vehicle using inheritance.
        /// </summary>
        /// <param name="vehicle">The vehicle object.</param>
        static void DisplayVehicleInfo(Vehicle vehicle)
        {
            Console.WriteLine($"{vehicle.Brand} {vehicle.Model}: {vehicle.GetVehicleInfo()}");
        }
    }

    /// <summary>
    /// Base class representing a vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets the model of the vehicle.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the brand of the vehicle.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class with the specified model and brand.
        /// </summary>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="brand">The brand of the vehicle.</param>
        public Vehicle(string model, string brand)
        {
            Model = model;
            Brand = brand;
        }

        /// <summary>
        /// Gets information about the vehicle.
        /// </summary>
        /// <returns>Information about the vehicle.</returns>
        public virtual string GetVehicleInfo()
        {
            return "Generic vehicle information.";
        }
    }

    /// <summary>
    /// Class representing a car, inheriting from the base vehicle class.
    /// </summary>
    public class Car : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class with the specified model and brand.
        /// </summary>
        /// <param name="model">The model of the car.</param>
        /// <param name="brand">The brand of the car.</param>
        public Car(string model, string brand) : base(model, brand)
        {
        }

        /// <summary>
        /// Overrides the base method to get specific information about the car.
        /// </summary>
        /// <returns>Information about the car.</returns>
        public override string GetVehicleInfo()
        {
            return "This is a car.";
        }
    }

    /// <summary>
    /// Class representing a bicycle, inheriting from the base vehicle class.
    /// </summary>
    public class Bicycle : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bicycle"/> class with the specified model and brand.
        /// </summary>
        /// <param name="model">The model of the bicycle.</param>
        /// <param name="brand">The brand of the bicycle.</param>
        public Bicycle(string model, string brand) : base(model, brand)
        {
        }

        /// <summary>
        /// Overrides the base method to get specific information about the bicycle.
        /// </summary>
        /// <returns>Information about the bicycle.</returns>
        public override string GetVehicleInfo()
        {
            return "This is a bicycle.";
        }
    }

}
