using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter a two numbers");
        int number1 = int.Parse(Console.ReadLine());
        int number2 = int.Parse(Console.ReadLine());
        int length = 0;
        int gcd = 0;
        if (number1>number2)
        {
            length = number1;
        }
        if (number1 < number2)
        {
            length = number2;
        }

        for (int i = 1; i <= length; i++)
        {
            if (number1 % i == 0 && number2 % i == 0)
            {
                gcd = i;
            }   
        }
        Console.WriteLine();
        Console.WriteLine("The GCD of {0} and {1} is: {2}",number1,number2,gcd);
        Console.WriteLine();
    }
}

