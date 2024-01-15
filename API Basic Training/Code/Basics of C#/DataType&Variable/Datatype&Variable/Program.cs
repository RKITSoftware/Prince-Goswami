using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datatype_Variable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region DataTypes
            int myNum = 5;               // Integer (whole number)
            double myDoubleNum = 5.99D;  // Floating point number
            char myLetter = 'D';         // Character
            bool myBool = true;          // Boolean
            string myText = "Hello";     // String
            long myLong = 1212121212121; // Long
            #endregion

            #region Type Casting

                #region Implicit Conversion

                Console.WriteLine("Implicit Conversion");

                int numInt = 500;
        
                // get type of numInt
                Type n = numInt.GetType();

                // Implicit Conversion
                double numDouble = numInt;

                // get type of numDouble
                Type n1 = numDouble.GetType();

                // Value before conversion
                Console.WriteLine("numInt value: " + numInt);
                Console.WriteLine("numInt Type: " + n);

                // Value after conversion
                Console.WriteLine("Value after conversion");
                Console.WriteLine("numDouble value: " + numDouble);
                Console.WriteLine("numDouble Type: " + n1);

                #endregion

                #region Explicit Conersion
            
                Console.WriteLine("Explicit Conversion");
                double dbl = 1.23;

                // Explicit casting
                int integer = (int)dbl;

                // Value before conversion
                Console.WriteLine("Original double Value: " + dbl);

                // Value after conversion
                Console.WriteLine("Value after conversion");
                Console.WriteLine("Converted int Value: " + integer);
                Console.ReadLine();
                #endregion
    
            #endregion
        }
    }
}
