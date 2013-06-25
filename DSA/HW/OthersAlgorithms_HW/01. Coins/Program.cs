

using System;
using System.Collections.Generic;
using System.Linq;

namespace T1.Coins
{
    class CoinsProgram
    {
        static void Main()
        {
            Console.WriteLine("Given infinite number of coins (1, 2 and 5) and end value N, write a program");
            Console.WriteLine("to give the number of coins needed so that their sum equals N");

            int realAmount = 33;
            int[] denominations = { 1, 2, 5 };
            int[] coins = new int[denominations.Length];
            Console.WriteLine("\nGreedy changing values for sum={0}: \n", realAmount);
            greedyChanges(realAmount, coins, denominations);
            showgreedyResult(denominations, coins);
        }

        //Calculate the minimum number of units for given amount (Greedy Strategy)
        private static void greedyChanges(int amount, int[] coin, int[] denomination)
        {
            int counter;
            Array.Sort(denomination, 1, denomination.Length - 1);
            for (int i = 0; i < coin.Length; i++)
            {
                coin[i] = 0;
            }
            for (counter = coin.Length - 1; counter >= 0 & amount > 0; counter--)
            {
                coin[counter] = amount / denomination[counter];
                amount -= coin[counter] * denomination[counter];
            }
        }

        //Display the final output on the Console (Greedy Results)
        private static void showgreedyResult(int[] denominations, int[] coins)
        {
            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i] > 0)
                    Console.WriteLine("Number of " + denominations[i] + "s : " + coins[i]);
            }
        }
    }
}

