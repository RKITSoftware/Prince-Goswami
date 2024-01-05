using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Single Array
            Console.WriteLine("Single Array\n");

            //Declaring an Array
            int[] a = { 1, 2, 3 };
            int[] b = new int[3];
            int i;

            //accesing Array element
            Console.Write("First element of Array a : ");
            Console.WriteLine(a[0]);

            //iterate Array
            for(  i = 0 ; i < b.Length ;  i++)
            {
                b[i] = i*2;
            }
            Console.Write("Elements of Array b : ");
            foreach (int key in b) {
                Console.Write(key+", ");
            }
            #endregion

            #region MultiDimesional Array

            Console.WriteLine("\n\nMultidimensional Array\n");

            //Declaration
            int[,] mulA = new int[5, 2] { { 0, 0 }, { 1, 2 }, { 2, 4 }, { 3, 6 }, { 4, 8 } };
            
            //Accesing Array elements
            int val = mulA[2, 1]  , j;

            //Iterate Array
            for (i = 0; i < 5; i++)
            {

                for (j = 0; j < 2; j++)
                {
                    Console.WriteLine("a[{0},{1}] = {2}", i, j, mulA[i, j]);
                }
            }
            #endregion

            #region Jagged Array

            Console.WriteLine("\n\nJagged Array\n");


            int[][] scores = new int[2][] { new int[] { 92, 93, 94 }, new int[] { 85, 66, 87, 88 } };

            /* output each array element's value */
            for (i = 0; i < 2; i++)
            {
                Console.Write("scores of team {0} = ",i+1);
                foreach(int key in scores[i])
                {
                    Console.Write("{0}, ",key);
                }
                Console.WriteLine();
            }
            #endregion
        }
    }
}
