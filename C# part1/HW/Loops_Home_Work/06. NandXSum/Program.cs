using System;


class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter N and X");
        int n = int.Parse(Console.ReadLine());
        int x = int.Parse(Console.ReadLine());
        decimal sum = 1M;
        decimal fakt = 1M;
        decimal divisor;
        for (int i = 1; i <= n; i++)
        {
            fakt *= i;
            divisor = (decimal)Math.Pow(x,i); 
            sum += fakt / divisor;
        }
        Console.WriteLine(sum);
    }
}
