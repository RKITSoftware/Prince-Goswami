using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesOfClass
{
    /// <summary>
    /// This is a partial class for additional functionality of the Penguin.
    /// </summary>
    public partial class Penguin : AbstractAnimal
    {
        /// <summary>
        /// Gets the additional behavior of the penguin.
        /// </summary>
        public void Swim()
        {
            Console.WriteLine("Penguin is swimming!");
        }
    }

    /// <summary>
    /// This is the second part of the partial class representing a Penguin in the zoo.
    /// </summary>
    public partial class Penguin
    {
        /// <summary>
        /// Gets the sound of the penguin.
        /// </summary>
        /// <returns>The sound of the penguin.</returns>
        public override string GetSound()
        {
            return "Honk";
        }
    }

}
