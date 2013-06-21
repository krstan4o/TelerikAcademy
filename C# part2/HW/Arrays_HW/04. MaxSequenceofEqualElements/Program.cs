using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter the array size");
        int size = int.Parse(Console.ReadLine());
        int[] numbers = new int[size];
        int count = 1;
        int bestCount = 0;
        int element = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i < numbers.Length-1; i++)
        {
            if (numbers[i] == numbers[i + 1])
            {
                count++;
                if (count>=bestCount)
                {
                    bestCount = count;
                    element = numbers[i];
                }
            }
            else 
            {
                count = 1;
            }
        }
        Console.WriteLine();
        Console.WriteLine("The max sequence of equal elements in the array is:\n");
        Console.Write("{");
        for (int i = 0; i < bestCount; i++)
        {
            if (i==bestCount-1)
            {
                 Console.Write("{0}",element);
                 break;
            }
            Console.Write("{0},",element);
        }
        if (bestCount==0)
        {
            Console.Write("There is no equal elements in the array.");
        }
        Console.WriteLine("}");
        Console.WriteLine();
    }
}
