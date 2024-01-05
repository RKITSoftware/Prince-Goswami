using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methods
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Enter two numbers :");
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());

            NumberOperation numberOperation = new NumberOperation();
            Console.WriteLine("Maximum Number is "+numberOperation.FindMax(a, b));

        }
    }
    class NumberOperation
    {
        public int FindMax(int num1, int num2)
        {
            int result;

            if (num1 > num2)
                result = num1;
            else
                result = num2;
            return result;
        }

    }
}
