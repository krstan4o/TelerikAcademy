using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter a number for matrix");
        int number = int.Parse(Console.ReadLine());
        int n = 1;
        if (number <= 0 || number >= 20)
        {
            Console.WriteLine("Please enter a number bigger than Zero");
            return;
        }
        else
        {
            for (int i = 0; i < number; i++)
            {
                n = i + 1;
                for (int j = 0; j < number; j++)
                {
                    Console.Write(" {0} ",n);
                    n += 1;
                }
                Console.WriteLine();
            }
        }
    }
}

