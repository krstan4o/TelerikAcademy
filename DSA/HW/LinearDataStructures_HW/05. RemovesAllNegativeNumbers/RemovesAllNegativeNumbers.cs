using System;
using System.Collections.Generic;
using System.Linq;

class RemovesAllNegativeNumbers
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter a sequence of positive int numbers - each number on one line\n" +
              "The program stops when empty line is entered");
        string inputLine = Console.ReadLine();
        List<int> numbers = new List<int>();
        while (inputLine != string.Empty)
        {
            int number = int.Parse(inputLine);
            numbers.Add(number);
            inputLine = Console.ReadLine();
        }
        
        numbers.RemoveNegativeNumbers();

        Console.WriteLine("The same sequence with removed negative numbers: ");
        foreach (int number in numbers)
        {
            Console.Write(number + " ");
        }
    }
}

