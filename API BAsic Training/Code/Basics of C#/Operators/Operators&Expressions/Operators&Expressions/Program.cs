using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Operators_Expressions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Operators

            int a, b;
            Console.WriteLine("Enter two numbers :");
            a = int.Parse(Console.ReadLine());
            b = int.Parse(Console.ReadLine());

            #region Arithmetic Operators

            Console.WriteLine("Arithmetic Operators\n");

            int result;
         

            //Addition
            result = a + b;
            Console.WriteLine("Addition " + result);

            //Substraction
            result = a - b;
            Console.WriteLine("Substraction: " + result);

            //Multiplication
            result = a * b;
            Console.WriteLine("Multiplication : " + result);

            //Division
            result = a / b;
            Console.WriteLine("Division : " + result);

            //Modulus
            result = a % b;
            Console.WriteLine("Modulus : " + result);

            #endregion

            #region Relational Operators
         
            Console.WriteLine("\nRelational Operators\n");

            bool resultR;

            //Equal To Operator
            resultR = (a == b);
            Console.WriteLine("Equal to :" + resultR);

            //Not Equal To Operator
            resultR = (a != b);
            Console.WriteLine("Not Equal to :" + resultR);

            //Grater than Operator
            resultR = (a > b);
            Console.WriteLine("Greater then :" + resultR);

            //less then Operator
            resultR = (a < b);
            Console.WriteLine("less then :" + resultR);

            //Greater then equal To Operator
            resultR = (a >= b);
            Console.WriteLine("Greater then equal to :" + resultR);

            //Less then equal To Operator
            resultR = (a <= b);
            Console.WriteLine("less then equal to :" + resultR);
            #endregion

            #region Logical Operators

            Console.WriteLine("\nLogical Operators\n");

            bool resultL;

            //And operator
            resultL = (a == b) && a > 5;
            Console.WriteLine("is {0} equal to {1} and greater then 5? {2}",a,b,resultL);

            //Or operator
            resultL = (a == b) || a > 5;
            Console.WriteLine("is {0} equal to {1} or greater then 5? {2}",a,b,resultL);

            #endregion

            #region Bitwise Operators

            Console.WriteLine("\nBitwise Operators\n");

            // Bitwise AND Operator
            result = a & b;
            Console.WriteLine("Bitwise AND: " + result);

            // Bitwise OR Operator
            result = a | b;
            Console.WriteLine("Bitwise OR: " + result);

            // Bitwise aOR Operator
            result = a ^ b;
            Console.WriteLine("Bitwise aOR: " + result);

            // Bitwise AND Operator
            result = ~a;
            Console.WriteLine("Bitwise Complement: " + result);

            // Bitwise LEFT SHIFT Operator
            result = a << 2;
            Console.WriteLine("Bitwise Left Shift: " + result);

            // Bitwise RIGHT SHIFT Operator
            result = a >> 2;
            Console.WriteLine("Bitwise Right Shift: " + result);

            #endregion

            #region Assignment Operators

            Console.WriteLine("\nAssignment Operators\n");

            int x = a;
            // it means x = x - 5
            x -= 5;
            Console.WriteLine("Subtract Assignment: " + x);

            // initialize x again
            x = a;

            // it means x = x * 5
            x *= 5;
            Console.WriteLine("Multiply Assignment: " + x);

            // initialize x again
            x = a;

            // it means x = x / 5
            x /= 5;
            Console.WriteLine("Division Assignment: " + x);

            // initialize x again
            x = a;

            // it means x = x % 5
            x %= 5;
            Console.WriteLine("Modulo Assignment: " + x);

            // initialize x again
            x = a;

            // it means x = x << 2
            x <<= 2;
            Console.WriteLine("Left Shift Assignment: " + x);

            // initialize x again
            x = a;

            // it means x = x >> 2
            x >>= 2;
            Console.WriteLine("Right Shift Assignment: " + x);

            // initialize x again
            x = a;

            // it means x = x >> 4
            x &= 4;
            Console.WriteLine("Bitwise AND Assignment Operator: " + x);

            // initialize x again
            x = a;

            // it means x = x >> 4
            x ^= 4;
            Console.WriteLine("Bitwise Exclusive OR Assignment: " + x);

            // initialize x again
            x = a;

            // it means x = x >> 4
            x |= 4;
            Console.WriteLine("Bitwise Inclusive OR Assignment: " + x);

            #endregion

            #region Conditional Operators\

            Console.WriteLine("\nConditional Operators\n");

            //Which is greater using Conditional Operator
            result = a > b ? a : b; 
            Console.WriteLine("Which is greater : " + result);
            #endregion

            #endregion


        }
    }
}
