using System;

namespace ClassesAndOOP
{
    class Polymorphism
    {
        /// <summary>
        /// Demonstrates polymorphism by displaying the sound made by different animals.
        /// </summary>
        public static void PolymorphismExample()
        {
            Console.WriteLine("\nPolymorphism Example:");
            Animal objCat = new Cat("Whiskers");
            Animal objDog = new Dog("Buddy");

            DisplayAnimalSound(objCat);
            DisplayAnimalSound(objDog);

            Console.WriteLine();
        }

        /// <summary>
        /// Displays the sound made by an animal.
        /// </summary>
        /// <param name="animal">The animal object.</param>
        static void DisplayAnimalSound(Animal animal)
        {
            Console.WriteLine($"{animal.Name} says: {animal.MakeSound()}");
        }
    }

    /// <summary>
    /// Represents an animal.
    /// </summary>
    public abstract class Animal
    {
        /// <summary>
        /// Gets the name of the animal.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the Animal class with the specified name.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        protected Animal(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Makes the sound of the animal.
        /// </summary>
        /// <returns>The sound made by the animal.</returns>
        public abstract string MakeSound();
    }

    /// <summary>
    /// Represents a cat, which is an animal.
    /// </summary>
    public class Cat : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Cat class with the specified name.
        /// </summary>
        /// <param name="name">The name of the cat.</param>
        public Cat(string name) : base(name)
        {
        }

        /// <summary>
        /// Makes the sound of the cat.
        /// </summary>
        /// <returns>The sound made by the cat.</returns>
        public override string MakeSound()
        {
            return "Meow!";
        }
    }

    /// <summary>
    /// Represents a dog, which is an animal.
    /// </summary>
    public class Dog : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Dog class with the specified name.
        /// </summary>
        /// <param name="name">The name of the dog.</param>
        public Dog(string name) : base(name)
        {
        }

        /// <summary>
        /// Makes the sound of the dog.
        /// </summary>
        /// <returns>The sound made by the dog.</returns>
        public override string MakeSound()
        {
            return "Woof!";
        }
    }
}