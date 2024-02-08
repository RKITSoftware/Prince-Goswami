using System;


namespace TypesOfClass
{
    /// <summary>
    /// This is an abstract class representing a generic animal in the zoo.
    /// </summary>
    public abstract class AbstractAnimal
    {
        /// <summary>
        /// Gets the sound the animal makes.
        /// </summary>
        /// <returns>The sound of the animal.</returns>
        public abstract string GetSound();

        /// <summary>
        /// Display method to show information about the animal.
        /// </summary>
        public void Display()
        {
            Console.WriteLine($"Type: {this.GetType().Name}, Sound: {GetSound()}");
        }
    }
}