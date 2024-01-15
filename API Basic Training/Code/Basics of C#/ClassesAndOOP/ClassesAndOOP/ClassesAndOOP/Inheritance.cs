using System;

namespace ClassesAndOOP
{
    #region Vehicle 
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
    #endregion

    #region Interface
    /// <summary>
    /// Interface for vehicles that can be driven.
    /// </summary>
    public interface IDrivable
    {
        void Drive();
    }

    /// <summary>
    /// Interface for vehicles that have a horn.
    /// </summary>
    public interface IHasHorn
    {
        void Honk();
    }
    #endregion

    #region Car Single, Multiple
    /// <summary>
    /// Represents a car. Inherits from Vehicle and implements IDrivable and IHasHorn.
    /// </summary>
    public class Car : Vehicle, IDrivable, IHasHorn
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
        /// Gets the vehicle information for a car.
        /// </summary>
        /// <returns>The vehicle information for a car.</returns>
        public override string GetVehicleInfo()
        {
            return "This is a car.";
        }

        /// <summary>
        /// Drives the car.
        /// </summary>
        public void Drive()
        {
            Console.WriteLine("Car is being driven.");
        }

        /// <summary>
        /// Honks the horn of the car.
        /// </summary>
        public void Honk()
        {
            Console.WriteLine("Car horn is honking.");
        }
    }
    #endregion

    #region  Hirearchcal

    #region Bicycle
    /// <summary>
    /// Represents a bicycle. Inherits from Vehicle and implements IDrivable.
    /// </summary>
    public class Bicycle : Vehicle, IDrivable
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
        /// Gets the vehicle information for a bicycle.
        /// </summary>
        /// <returns>The vehicle information for a bicycle.</returns>
        public override string GetVehicleInfo()
        {
            return "This is a bicycle.";
        }

        /// <summary>
        /// Drives the bicycle.
        /// </summary>
        public void Drive()
        {
            Console.WriteLine("Bicycle is being pedaled.");
        }
    }
    #endregion

    #region Flying
    /// <summary>
    /// Represents a vehicle that can fly. Inherits from Vehicle and implements IDrivable.
    /// </summary>
    public class FlyingVehicle : Vehicle, IDrivable
    {
        /// <summary>
        /// Initializes a new instance of the FlyingVehicle class.
        /// </summary>
        /// <param name="model">The model of the flying vehicle.</param>
        /// <param name="brand">The brand of the flying vehicle.</param>
        public FlyingVehicle(string model, string brand) : base(model, brand)
        {
        }

        /// <summary>
        /// Gets the vehicle information for a flying vehicle.
        /// </summary>
        /// <returns>The vehicle information for a flying vehicle.</returns>
        public override string GetVehicleInfo()
        {
            return "This is a flying vehicle.";
        }

        /// <summary>
        /// Drives the flying vehicle.
        /// </summary>
        public void Drive()
        {
            Console.WriteLine("Flying vehicle is in the air.");
        }
    }
    #endregion
    #endregion

    #region flyingCar Multilevel
    /// <summary>
    /// Represents a flying car. Inherits from Car and FlyingVehicle, combining interfaces.
    /// </summary>
    public class FlyingCar : Car, IDrivable
    {
        /// <summary>
        /// Initializes a new instance of the FlyingCar class.
        /// </summary>
        /// <param name="model">The model of the flying car.</param>
        /// <param name="brand">The brand of the flying car.</param>
        public FlyingCar(string model, string brand) : base(model, brand)
        {
        }

        /// <summary>
        /// Gets the vehicle information for a flying car.
        /// </summary>
        /// <returns>The vehicle information for a flying car.</returns>
        public override string GetVehicleInfo()
        {
            return "This is a flying car.";
        }

        /// <summary>
        /// Drives the flying car.
        /// </summary>
        new public void Drive()
        {
            Console.WriteLine("Flying car is in the air.");
        }
    }
    #endregion


    class Inheritance
    {
        /// <summary>
        /// Runs the inheritance example.
        /// </summary>
        public static void InheritanceExample()
        {
            Console.WriteLine("\nInheritance Example:");

            // Single Inheritance: Car inherits from Vehicle
            Car car = new Car("Sedan", "Toyota");
            DisplayVehicleInfo(car);
            car.Drive(); // Using specific behavior for cars
            car.Honk();  // Using specific behavior for cars

            // Single Inheritance: Bicycle inherits from Vehicle
            Bicycle bicycle = new Bicycle("Mountain Bike", "Trek");
            DisplayVehicleInfo(bicycle);
            bicycle.Drive(); // Using specific behavior for bicycles

            // Single Inheritance: FlyingVehicle inherits from Vehicle
            FlyingVehicle flyingVehicle = new FlyingVehicle("Jet", "Boeing");
            DisplayVehicleInfo(flyingVehicle);
            flyingVehicle.Drive(); // Using specific behavior for flying vehicles

            // Multiple Inheritance: FlyingCar inherits from Car and FlyingVehicle
            FlyingCar flyingCar = new FlyingCar("Flying Sedan", "FutureCars");
            DisplayVehicleInfo(flyingCar);
            flyingCar.Drive(); // Using specific behavior for flying cars

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
}