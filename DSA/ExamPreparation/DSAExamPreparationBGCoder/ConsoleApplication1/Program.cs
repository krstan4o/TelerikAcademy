using System;
using System.Collections.Generic;
using System.Numerics;
class Program
{
    static void Main()
    {
        //Finding number of Subsets Of wanted SUm

        long wantedSum = long.Parse(Console.ReadLine());
        int count = 0;
        int numberOfElements = int.Parse(Console.ReadLine());
        long[] elements = new long[numberOfElements];
        string subset = "";
        for (int i = 0; i < elements.Length; i++)
        {

            elements[i] = long.Parse(Console.ReadLine());
        }
        int maxSubsets = (int)Math.Pow(2, numberOfElements) - 1;

        for (int i = 1; i <= maxSubsets; i++)
        {
            subset = "";
            long checkingSum = 0;
            for (int j = 0; j <= numberOfElements; j++)
            {
                int mask = 1 << j;
                int nAndMask = i & mask;
                int bit = nAndMask >> j;
                if (bit == 1)
                {
                    checkingSum = checkingSum + elements[j];
                    subset = subset + " " + elements[j];
                }
            }
            if (checkingSum == wantedSum)
            {
                count++;
            }
        }
        Console.WriteLine(count);

     

        
    }
}

