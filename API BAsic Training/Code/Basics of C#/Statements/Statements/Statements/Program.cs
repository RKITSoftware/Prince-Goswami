using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Conditional
            int a = 10, b = 20;

            //If...Else
            Console.WriteLine("\nIf...Else\n");

            if (a >= b)
            {
                if (a > b)
                {
                    Console.WriteLine("{0} is greater then {1}", a, b);
                }
                else
                    Console.WriteLine("both numbers are equal");
            }
            else
            {
                Console.WriteLine("{0} is less then {1}", a, b);
            }

            //Switch Case
            Console.WriteLine("\nSwitch\n");

            Console.WriteLine("Enter day of the week : ");
            int n = int.Parse(Console.ReadLine());

            switch (n)
            {
                case 0: 
                    Console.WriteLine("Sunday");
                    break;
                case 1:
                    Console.WriteLine("Monday");
                    break;
                case 2:
                    Console.WriteLine("Tuesday");
                    break;
                case 3:
                    Console.WriteLine("Wednsday");
                    break;
                case 4:
                    Console.WriteLine("Thursday");
                    break;
                case 5:
                    Console.WriteLine("Friday");
                    break;
                case 6:
                    Console.WriteLine("Saturday");
                    break;
                default:
                    Console.WriteLine("Invalid number");
                    break;    
            }
            #endregion

            #region Looping
            Console.WriteLine("\n\n" +
                "Looping\n");

            int i = 5;
            //While
            Console.WriteLine("\nWhile Loop\n");

            while (i>0) {
                Console.WriteLine("While " + i--);
            };

            //Do...While
            Console.WriteLine("\nDo While\n");

            do
            {
                Console.WriteLine("Do While " + i--);
            } while ( i > 0);

            //For
            Console.WriteLine("\nFor Loop\n");

            for (i = 0; i < 5; i++)
                Console.WriteLine("For " + i);


            //Nested for
            Console.WriteLine("\nNested For\n");
            for(i = 0; i < 5; i++)
            {
                for(int j = 0;j < 5; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }

            #endregion
        }
    }
}
