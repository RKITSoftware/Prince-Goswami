using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassesAndOOP
{
    internal class Program
    {
        static void Main()
        {
            #region Class
            // Regular class example
            TypesOfClass.RegularClassExample();

            // Abstract class example
            TypesOfClass.AbstractClassExample();

            // Static class example
            TypesOfClass.StaticClassExample();

            // Sealed class example
            TypesOfClass.SealedClassExample();

            #endregion

            #region OOP

            // Encapsulation example
            Encapsulation.EncapsulationExample();

            // Inheritance example
            Inheritance.InheritanceExample();

            // Polymorphism example
            Polymorphism.PolymorphismExample();

            #endregion
            
            Console.ReadLine();
        }

    }
}

