using System;

namespace ClassesAndOOP
{

    class Polymorphism
    {
        public static void polymorphismRun()
        {
            Console.WriteLine("\nPolymorphism Example:");
            // Creating instances of different animals
            Animal objCat = new Cat("Whiskers");
            Animal objDog = new Dog("Buddy");

            // Displaying sounds of different animals using polymorphism
            DisplayAnimalSound(objCat);
            DisplayAnimalSound(objDog);

            Console.WriteLine();
        }

        /// <summary>
        /// Displays the sound of an animal using polymorphism.
        /// </summary>
        /// <param name="animal">The animal object.</param>
        static void DisplayAnimalSound(Animal animal)
        {
            Console.WriteLine($"{animal.Name} says: {animal.MakeSound()}");
        }
    }

    /// <summary>
    /// Base class representing an animal.
    /// </summary>
    public abstract class Animal
    {
        /// <summary>
        /// Gets the name of the animal.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animal"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        protected Animal(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Abstract method to make the animal sound.
        /// </summary>
        /// <returns>The sound of the animal.</returns>
        public abstract string MakeSound();
    }

    /// <summary>
    /// Class representing a cat, inheriting from the base animal class.
    /// </summary>
    public class Cat : Animal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cat"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the cat.</param>
        public Cat(string name) : base(name)
        {
        }

        /// <summary>
        /// Implementation of the abstract method to make the cat sound.
        /// </summary>
        /// <returns>The sound of the cat.</returns>
        public override string MakeSound()
        {
            return "Meow!";
        }
    }

    /// <summary>
    /// Class representing a dog, inheriting from the base animal class.
    /// </summary>
    public class Dog : Animal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dog"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the dog.</param>
        public Dog(string name) : base(name)
        {
        }

        /// <summary>
        /// Implementation of the abstract method to make the dog sound.
        /// </summary>
        /// <returns>The sound of the dog.</returns>
        public override string MakeSound()
        {
            return "Woof!";
        }
    }
}