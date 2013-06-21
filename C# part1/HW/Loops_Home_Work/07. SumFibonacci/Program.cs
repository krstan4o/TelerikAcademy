using System;
using System.Numerics;
class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter N member of Fibonacci sequence...");
        int n = int.Parse(Console.ReadLine());    
         if (n <= 0)
        {
            Console.WriteLine("Please enter a number bigger than Zero");
            return;
        }
         else
         {
           BigInteger[] arr =new BigInteger[n+1];
           arr[0] = 1;
           arr[1] = 1;
       
        
            for (int i = 2; i < n; i++)
            {
                arr[i] = arr[i - 2] + arr[i - 1];
            }
            Console.WriteLine("The {0} Fibonacci member is: {1}",n,arr[n - 1]);
        }
    }
}
