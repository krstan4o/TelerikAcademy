using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter a number N");
        int n = int.Parse(Console.ReadLine());

        Console.WriteLine("The numbers from 1 to N are:");

        for (int i = 1; i <= n; i++)
        {
            Console.WriteLine(i);
        }
    }
}

