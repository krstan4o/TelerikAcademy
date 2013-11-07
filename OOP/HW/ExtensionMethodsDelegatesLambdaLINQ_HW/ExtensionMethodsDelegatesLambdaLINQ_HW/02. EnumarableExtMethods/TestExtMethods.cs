using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace _02.EnumarableExtMethods
{
    class TestExtMethods
    {
        static void Main()
        {
            double[] arr = { 1.2, 2.5, 2.3, 3.9 };
            Console.WriteLine("The average of all elements in the array is:");
            Console.WriteLine(arr.Average());
            Console.WriteLine("Max element in array is:");
            Console.WriteLine(arr.Max());
            Console.WriteLine("Min element in array is:");
            Console.WriteLine(arr.Min());
            Console.WriteLine("The product of all elements in the array is:");
            Console.WriteLine(arr.Product());
            Console.WriteLine("The sum of all elements in the array is:");
            Console.WriteLine(arr.Sum());
        }
    }
}
