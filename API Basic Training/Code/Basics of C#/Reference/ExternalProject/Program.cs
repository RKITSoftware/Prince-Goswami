// Namespace declaration for the external project
using System;

namespace ExternalProject
{
    /// <summary>
    /// Entry point for the external project
    /// </summary>

    class Program
    {
        /// <summary>
        /// Main method, the starting point of the external project
        /// </summary>
        
        public static void Main(string[] args)
        {
            // Main method is currently empty
        }
    }

    /// <summary>
    /// Class representing external 
    /// </summary>
    public class External
    {
        /// <summary> 
        /// Static method to print a message indicating it's from another project
        /// </summary>
        public static void Print()
        {
            Console.WriteLine("This is a method from another project.");
        }
    }
}
