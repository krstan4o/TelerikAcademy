using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatePrimesAsync
{
    public static class PrimesCalculator
    {
        public static Task<List<int>> GetPrimesInRangeAsync(int rangeFirst, int rangeLast)
        {
            return Task.Run(
                () => GetPrimesInRange(rangeFirst, rangeLast)
                    );
        }

        public static List<int> GetPrimesInRange(int rangeFirst, int rangeLast)
        {
            List<int> primes = new List<int>();

            for (int number = rangeFirst; number < rangeLast; number++)
            {
                bool isPrime = true;
                for (int divider = 2; divider < number; divider++)
                {
                    if (number % divider == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(number);
                }
            }

            return primes;
        }
    }
}
