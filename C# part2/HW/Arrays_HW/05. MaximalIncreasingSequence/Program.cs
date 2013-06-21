using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter the array size");
        int size = int.Parse(Console.ReadLine());
        int[] numbers = new int[size];
        int count = 1;
        int bestCount = 1;
        int element = 0;
        int index = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i < numbers.Length - 1; i++)
        {
            if (numbers[i] < numbers[i + 1])
            {
                count++;
                if (count >= bestCount)
                {
                    bestCount = count;
                    element = numbers[i];
                    if (count == 2)
                    {
                        index = i;
                    }
                }
            }
            else
            {
                count = 1;
            }
        }
        Console.WriteLine();
        Console.WriteLine("The max sequence of increasing elements in the array is:\n");
        if (bestCount == 1)
        {
            Console.Write("There is no increasing sequence in the array.");
            return;
        }
        Console.Write("{");
        
        for (int i = index; i <= bestCount+index-1; i++)
        {
            if (i == bestCount+index-1)
            {
                Console.Write("{0}", numbers[i]);
                break;
            }
            Console.Write("{0},", numbers[i]);
        }
       
        Console.WriteLine("}");
        Console.WriteLine();
    }
}

