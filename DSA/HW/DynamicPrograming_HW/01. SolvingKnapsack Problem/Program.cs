using System;
using System.Collections.Generic;

namespace _01.KnapsackProblem
{
    class KnapsackProblem
    {
        static void Main()
        {
            Item[] myItems = new Item[]{
                                        new Item("beer", 3, 2),
                                        new Item("vodka", 8, 12),
                                        new Item("cheese", 4, 5),
                                        new Item("nuts", 1, 4),
                                        new Item("ham", 2, 3),
                                        new Item("wiskey", 8, 13)
            };
            int myBagCapacity = 10;

            List<Item> result = FindOptimalSolution(myItems, myBagCapacity);
            Console.WriteLine("Best choice: ");
            Console.WriteLine(String.Join("\n", result));
        }

        public static List<Item> FindOptimalSolution(Item[] items, int capacity)
        {

            int[,] valuesArray = new int[items.Length + 1, capacity + 1]; // * contains all items for rows and bag capacities for cols. Will keep the best values in each capacity of the bag.
            int[,] keepArray = new int[items.Length + 1, capacity + 1]; // * contains the same rows and cols like the valuesArray, but this keeps info if the current item is included in the best values for each bag capacity.

            for (int i = 1, itemsLen = items.Length + 1; i < itemsLen; i++) // * for each item (ex: beer, wiskey, vodka...)
            {
                for (int k = 1; k < capacity + 1; k++) // * for each capacity of the bag (ex: if max capacity = 5 -> 1,2,3,4,5 / adds 1)
                {
                    if (items[i - 1].Weigth <= k) // * the weigth of the current item (items[i-1]) is le(less than or equal) to the current bag capacity
                    {
                        int remainingSpace = (k) - items[i - 1].Weigth; // * remaining space - the difference between the weigth of the currentItem and the current capacity of the bag.

                        if (remainingSpace > 0)
                        {
                            int valueAbove = valuesArray[i - 1, k]; // * row above, same coll
                            int sumValue = items[i - 1].Value + valuesArray[i - 1, remainingSpace - 1]; //* currentItem.Value + value from above row, remainingSpace col.

                            if (valueAbove > sumValue) // * gets the biggest value
                            {
                                valuesArray[i, k] = valueAbove;
                                keepArray[i, k] = 0;
                            }
                            else
                            {
                                valuesArray[i, k] = sumValue;
                                keepArray[i, k] = 1;
                            }
                        }
                        else // * remaining space == 0
                        {
                            valuesArray[i, k] = items[i - 1].Value;
                            keepArray[i, k] = 1;
                        }
                    }
                }
            }

            List<Item> bestChoice = new List<Item>();

            int remainSpace = capacity;
            int item = items.Length;

            while (item >= 0)
            {
                int toBeAdded = keepArray[item, remainSpace - 1];

                if (toBeAdded == 1)
                {
                    bestChoice.Add(items[item - 1]);
                    remainSpace -= items[item - 1].Weigth;
                }

                item--;
            }

            return bestChoice;
        }
    }
}