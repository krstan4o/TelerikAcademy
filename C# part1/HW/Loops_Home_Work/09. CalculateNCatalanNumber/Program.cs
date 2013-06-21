using System;
using System.Numerics;
class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter a number");
        int n = int.Parse(Console.ReadLine());
        BigInteger fakt2n = 1;
        BigInteger faktn = 1;
        BigInteger faktn1 = 1;
        BigInteger result = 0;
        if (n < 0)
        {
            Console.WriteLine("Please enter a number >= \"0\"");
            return;
        }
        else
        {
            if (n==0)
            {
                Console.WriteLine("The 0 Catalan number is: 0");
                return;
            }
            for (int i = 1; i <= (2*n); i++)
            {
                fakt2n *= i;
            }
            for (int i = 1; i <= (n+1); i++)
            {
                faktn1 *= i;
            }
            for (int i = 1; i <= n; i++)
            {
                faktn *= i;
            }
            result = fakt2n / (faktn1 * faktn);
            Console.WriteLine("The {0} Catalan number is: {1}",n,result);
        }
    }
}
