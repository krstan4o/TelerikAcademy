using System;
using System.Numerics;
class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter a positive number");        
        int number = int.Parse(Console.ReadLine());
       
        BigInteger faktn = 1;
        int count = 0;
        
        for (int i = 1; i <= number; i++)
        {
            faktn *= i;
        }
        for (int i = 0; i <= int.MaxValue; i++)
        {
            if (faktn % 10 == 0)
            {
                count++;
                faktn /= 10;
            }
            else
            {              
                break;
            }
        }        
        Console.WriteLine("The number of last trailing zeroes in {0}! is: {1}",number,count);
    }
}

