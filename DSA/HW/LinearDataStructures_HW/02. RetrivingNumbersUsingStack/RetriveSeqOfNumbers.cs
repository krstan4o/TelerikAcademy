using System;
using System.Collections.Generic;
using System.Linq;

class RetriveSeqOfNumbers
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter how much numbers u want to enter: ");
        int numbersCount = int.Parse(Console.ReadLine());
        Console.WriteLine("Now enter {0} integer numbers.", numbersCount);
        Stack<int> numbers = new Stack<int>(numbersCount);
        
        for (int i = 0; i < numbersCount; i++)
        {
            Console.Write("Number {0}: ", i + 1);
            int number = int.Parse(Console.ReadLine());
            numbers.Push(number);
        }

        Console.WriteLine("\nNow lets retrive the numbers: ");
        int numberCount = 0;
        while (numbers.Count!=0)
        {
            numberCount++;
            Console.Write("Number {0}: {1}\n", numberCount, numbers.Pop());
        }
    }
}

