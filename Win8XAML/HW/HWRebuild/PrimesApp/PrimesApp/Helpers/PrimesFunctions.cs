using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimesApp.Helpers
{
    public static class PrimesFunctions
    {

        public static async Task<List<string>> CalculatePrimesRangeAsync(int start, int end)
        {
            return await Task.Run(() => CalculatePrimesRange(start, end));
        }

        public static List<string> CalculatePrimesRange(int start, int end) 
        {
            List<string> resultPrimes = new List<string>();

            for (int i = start; i <= end; i++)
            {
                if (IsPrime(i))
                {
                    resultPrimes.Add(i.ToString()); // concat with the second and add to list
                }
            }
            return resultPrimes;

        }

        private static bool IsPrime(int number) 
        {
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static async Task<string> GetPrimeBeginsWithDiggitAsync(string number)
        {
           
            return await Task.Run(()=>
            {
                int diggit = number[number.Length - 1];

                return "";
            });
        }
    }
}
