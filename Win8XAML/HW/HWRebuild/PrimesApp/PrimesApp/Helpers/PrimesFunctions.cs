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

        public static bool IsPrime(int number) 
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

        public static async Task<List<string>> GetPrimesBeginsWithDiggitAsync(List<string> primes)
        {
           
            return await Task.Run(()=>
            {
                List<string> result = new List<string>();
                foreach (var prime in primes)
                {
                    int lastDiggit = int.Parse(prime) % 10;

                    if (lastDiggit.ToString() != prime && IsPrime(lastDiggit))
                    {
                        result.Add(prime + lastDiggit);
                        continue;
                    }

                    bool isPartnerFound = false;
                    int from = lastDiggit * 10;
                    int to = from + 9;

                    while (!isPartnerFound)
                    {
                        List<string> partners = CalculatePrimesRange(from, to);
                        if (partners.Count > 0)
                        {
                            result.Add(prime + partners[0]);
                            isPartnerFound = true;
                            break;
                        }
                        else
                        {
                            from *= 10;
                            to += 9;
                        }
                    }
                }
                return result;
            });
        }

        internal static async Task<List<string>> GetPrimesOrNoPrimesResult(List<string> primesWithPartners, bool primesOnly)
        {
            return await Task.Run(() =>
            {
                List<string> result = new List<string>();

                foreach (var prime in primesWithPartners)
                {
                    if ( IsPrime(int.Parse(prime)) && primesOnly)
                    {
                        result.Add(prime);
                    }
                    else if (!IsPrime(int.Parse(prime)) && !primesOnly)
                    {
                         result.Add(prime);
                    }
                }

                return result; 
            
            });
        }
    }
}
