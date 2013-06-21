using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter the sum u want to search for...");
        int sum = int.Parse(Console.ReadLine());
        Console.Write("Please enter array size:");
        int size = int.Parse(Console.ReadLine());
        int[] array = new int[size];
        Console.WriteLine("Now enter {0} elements", size);
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = int.Parse(Console.ReadLine());
        }

       
            
                int max = array[0], maxEnd = array[0];
                int longSequence = 1, currentSequence = 1;
                int start = 0, startTemp = 0;
                //Kadane's algorithm
                for (int i = 1; i < array.Length; ++i)
                {
                    if (array[i] + maxEnd <= sum)
                    {
                        maxEnd = array[i] + maxEnd;
                        currentSequence++;
                    }

                    else
                    {
                        maxEnd = array[i];
                        startTemp = i;
                        currentSequence = 1;
                    }
                    if (maxEnd >= sum)
                    {
                        max = maxEnd;
                        longSequence = currentSequence;
                        start = startTemp;
                    }
                }
                for (int i = start; i < start + longSequence; ++i)
                {
                    Console.Write("{0} ", array[i]);
                }
            }
}

