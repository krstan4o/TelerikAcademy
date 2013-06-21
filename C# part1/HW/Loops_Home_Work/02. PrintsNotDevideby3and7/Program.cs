using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter a number N");
        int n = int.Parse(Console.ReadLine());


        Console.WriteLine("The numbers from 1 to N, that are not divisible by 3 and 7 are:");

        for (int i = 1; i <= n; i++)
        {
            if (i % 3 != 0 && i % 7 != 0)
            {
                Console.WriteLine(i);
            }
           
        }

    }
}

