


using classLibrary;
using ExternalProject;

namespace ReferenceDemo
{
    /// <summary>
    /// A demo that represents Refernce technique
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // this will call a method from external referenced class
            External.Print();

            // this will call a method from external referenced library
            ClassLib.Print();

        }
    }
}
