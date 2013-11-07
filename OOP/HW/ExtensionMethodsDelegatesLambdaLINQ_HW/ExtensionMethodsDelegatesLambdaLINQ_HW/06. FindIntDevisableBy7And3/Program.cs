using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.FindIntDevisableBy7And3
{
    class Program
    {
        static void Main()
        {
            int[] arr = { 1, 2, 3, 4, 2, 21};

            var findIntDevideBy3And7 = arr.Where(x => x % 3 == 0 && x % 7 == 0);
            Console.WriteLine("Find all int numbers in array that are devidable by 3 and 7:");
            foreach (var item in findIntDevideBy3And7)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            var findAgain =
                from number in arr
                where number % 3 == 0 && number % 7 == 0
                select number;
            Console.WriteLine("The same like above but using LINQ:");
            foreach (var item in findAgain)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}
