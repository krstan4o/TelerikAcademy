using System;
using System.Numerics;

class Program
{
    static void Main()
    {
        //The formula of the program is: N!*K! / (K-N)! 

          Console.WriteLine("Please enter N and K");
        
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());
        BigInteger result = 1;
        BigInteger factk = 1;
        BigInteger factn = 1;
        BigInteger factnandk = 1;
        if (n <= 1 || k <= n)
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
            for (int v = 1; v <= k - n; v++)
            {
                factnandk = factnandk * v;
            }
            result = (factn * factk) / factnandk;

            Console.WriteLine("N!*K! / (K-N)! = {0}",result);

        }
    }
}

