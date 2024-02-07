using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classLibrary
{
    /// <summary>
    /// Class Library version 4.8
    /// </summary>
    public class ClassLib
    {
        /// <summary>
        /// Print method that prints a message when it is called
        /// </summary>
        public static void Print()
        {
            Console.WriteLine("This is method from external class library.");
        }

        public static void print()
        {
            throw new NotImplementedException();
        }
    }
}
