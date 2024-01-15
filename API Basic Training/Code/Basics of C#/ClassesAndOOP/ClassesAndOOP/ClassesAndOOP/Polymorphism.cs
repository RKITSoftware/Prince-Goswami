﻿using System;

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
            Animal objDuck = new Duck("Quak");

            DisplayAnimalSound(objCat);
            DisplayAnimalSound(objDog);
            DisplayAnimalSound(objDuck, 3);

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

        /// <summary>
        /// Displays the sound made by an animal on repeated count.
        /// </summary>
        /// <param name="animal">The animal object.</param>
        // Method overload 2
        static void DisplayAnimalSound(Animal animal, int repeatCount)
        {
            Console.Write($"{animal.Name} says:");
            for (int i = 0; i < repeatCount; i++)
            {
                Console.Write($" {animal.MakeSound()}");
            }
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

        /// <summary>
        /// Makes the sound of the dog.
        /// </summary>
        /// <param>The sound use want made by the dog.</param>
        /// <returns>The sound use want made by the dog.</returns>
        ///
        //public override string MakeSound(string sound = "bark")
        //{
        //    return sound;
        //}
    }
    /// <summary>
    /// Represents a Duck, which is an animal.
    /// </summary>
    public class Duck : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Duck class with the specified name.
        /// </summary>
        /// <param name="name">The name of the Duck.</param>
        public Duck(string name) : base(name)
        {
        }

        /// <summary>
        /// Makes the sound of the Duck.
        /// </summary>
        /// <returns>The sound made by the Duck.</returns>
        public override string MakeSound()
        {
            return "Quak!";
        }

    }
}

