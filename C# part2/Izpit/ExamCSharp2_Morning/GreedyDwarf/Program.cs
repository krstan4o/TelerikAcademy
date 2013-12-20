using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private static int[] valley;
    private static bool[] visitedValley;
    private static int maxNumberOfCoins = 0;
    private static int[][] patterns;

    static void Main()
    {
        valley = Console.ReadLine()
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();
        visitedValley = new bool[valley.Length];
        int patternsCount = int.Parse(Console.ReadLine());
        patterns = new int[patternsCount][];
        for (int i = 0; i < patternsCount; i++)
        {
            patterns[i] = Console.ReadLine()
                                 .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(int.Parse)
                                 .ToArray();
        }
        maxNumberOfCoins = valley[0];
        Solve();
        Console.WriteLine(maxNumberOfCoins);
    }

    private static void Solve() 
    {
        for (int i = 0; i < patterns.Length; i++)
        {
            int tempSum = valley[0];
            visitedValley[0] = true;
            int lastVisitedIndex = 0;
            int k = 0;

            while (IsInValley(lastVisitedIndex + patterns[i][k]) && !visitedValley[lastVisitedIndex + patterns[i][k]])
            { 
                tempSum += valley[lastVisitedIndex + patterns[i][k]];
                lastVisitedIndex += patterns[i][k];
                visitedValley[lastVisitedIndex] = true;
                k++;
                if (k >= patterns[i].Length)
                {
                    k = 0;
                }
            }
            visitedValley = new bool[valley.Length];
            if (tempSum >= maxNumberOfCoins)
            {
                maxNumberOfCoins = tempSum;
            }
        }
    }

    private static bool IsInValley(int index) 
    {
        if (index >= 0 && index < valley.Length)
        {
            return true;
        }
        return false;
    }
}

