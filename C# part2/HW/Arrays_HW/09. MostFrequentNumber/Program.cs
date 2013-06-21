using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter the array size");
        int size = int.Parse(Console.ReadLine());
        int count = 0;
        int bestCount = 0;
        int element = 0;
        int element1 = 0;
        int[] numbers = new int[size];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            for (int j = 0; j < numbers.Length; j++)
            {
                if (numbers[i]==numbers[j])
                {
                    count++;
                    if (count>bestCount)
                    {
                        bestCount = count;
                        element = numbers[i];
                    }
                    else if (count == bestCount)
                    {
                        bestCount = count;
                        element1 = numbers[i];
                    }
                }
            }
            count = 0;
        }
        Console.WriteLine("\nThe most frequent number in the array is: \n");
        if (element == element1)
        {
            Console.WriteLine("{0}({1} times)\n", element, bestCount);

        }
        else
        {
            Console.WriteLine("{0}({1} times) && {2}({1} times)\n", element, bestCount, element1);
        }
    }
}

