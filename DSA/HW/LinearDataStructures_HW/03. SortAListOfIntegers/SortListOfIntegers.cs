using System;
using System.Collections.Generic;
using System.Linq;

class SortListOfIntegers
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter a sequence int numbers - each number on one line\n" +
               "The program stops when empty line is entered");
        string inputLine = Console.ReadLine();
        List<int> numbers = new List<int>();
        while (inputLine != string.Empty)
        {
            int number = int.Parse(inputLine);
            numbers.Add(number);
            inputLine = Console.ReadLine();
        }

        Console.WriteLine("\nThe sorted sequence of numbers is: ");
        numbers.Sort();
        foreach (int number in numbers)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine("\n");
    }
}

