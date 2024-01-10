using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesAndOOP
{
    internal class Encapsulation
    {
        public static void EncapsulationExample()
        {

            Console.WriteLine("\nEncapsulation Example:");

            // Creating an instance of the encapsulated class
            EncapsulationDemo encapsulationDemo = new EncapsulationDemo();

            // Accessing encapsulated properties and methods
            encapsulationDemo.SetName("Prince Goswami");
            encapsulationDemo.SetAge(22);

            Console.WriteLine($"Name: {encapsulationDemo.GetName()}, Age: {encapsulationDemo.GetAge()}");


            // Console.WriteLine(encapsulationDemo.age);

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Class demonstrating encapsulation principles.
    /// </summary>
    public class EncapsulationDemo
    {
        
        private string name; // Private field for name
        private int age;     // Private field for age

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        /// <returns>The name of the object.</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Sets the name of the object.
        /// </summary>
        /// <param name="newName">The new name to set.</param>
        public void SetName(string newName)
        {
            if( newName == null ) {
                Console.WriteLine("Enter valid name..!!");
            }
            name = newName;
        }

        
        /// <summary>
        /// Gets the age of the object.
        /// </summary>
        /// <returns>The age of the object.</returns>
        public int GetAge()
        {
            return age;
        }

        /// <summary>
        /// Sets the age of the object.
        /// </summary>
        /// <param name="newAge">The new age to set.</param>
        public void SetAge(int newAge)
        {
            if (newAge >= 0)
            {
                age = newAge;
            }
            else
            {
                Console.WriteLine("Invalid age value.");
            }
        }
    }

}
