using System;
using System.Numerics;
class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter K and N");
        int k = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());
        BigInteger factk = 1;
        BigInteger factn = 1;
        BigInteger devide;
        if (k <= 1 || n <= k)
        {
            Console.WriteLine("Please enter K bigger than \"1\" and N bigger than K");
        }
        else
        {
            for (int i = 1; i <= k; i++)
            {
                factk = factk * i;
            }

            for (int j = 1; j <= n; j++)
            {
                factn = factn * j;
            }

            devide = factn / factk;
            Console.WriteLine("N!/K!= {0}",devide);
        }
    }
}

