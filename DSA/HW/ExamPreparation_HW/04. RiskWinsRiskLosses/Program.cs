using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static int[] powerOfTen = new int[5];
    static bool[] used = new bool[100000];
    static bool[] restriktedCombinations = new bool[100000];
    static int startCombination;
    static int endCombination;

    static void Main()
    {
        startCombination = int.Parse(Console.ReadLine());
        endCombination = int.Parse(Console.ReadLine());
        int restrictedCombinationsCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < restrictedCombinationsCount; i++)
        {
            int restrictedCombination = int.Parse(Console.ReadLine());
            restriktedCombinations[restrictedCombination] = true;
        }

        powerOfTen[0] = 1;
        for (int i = 1; i < 5; i++)
        {
            powerOfTen[i] = powerOfTen[i - 1] * 10;
        }
        int result = Solve();
        Console.WriteLine(result);
    }

    static int Solve()
    {
        int level = 0;
        Queue<int> nodes = new Queue<int>();
        nodes.Enqueue(startCombination);

        while (nodes.Count > 0)
        {
            Queue<int> nextLevelNodes = new Queue<int>();
            level++;

            while (nodes.Count > 0)
            {
                int node = nodes.Dequeue();

                if (node == endCombination)
                {
                    return level - 1;
                }
                //press left button
                for (int i = 0; i < 5; i++)
                {
                    int newNode = node;
                    int diggit = (node / powerOfTen[i]) % 10;
                    if (diggit == 9)
                    {
                        newNode -= 9 * powerOfTen[i];
                    }
                    else
                    {
                        newNode += powerOfTen[i];
                    }
                    if (used[newNode])
                        continue;
                    if (restriktedCombinations[newNode])
                        continue;

                    used[newNode] = true;
                    nextLevelNodes.Enqueue(newNode);
                }

                //pressing right button
                for (int i = 0; i < 5; i++)
                {
                    int newNode = node;
                    int diggit = (node / powerOfTen[i]) % 10;
                    if (diggit == 0)
                    {
                        newNode += 9 * powerOfTen[i];
                    }
                    else
                    {
                        newNode -= powerOfTen[i];
                    }
                    if (used[newNode])
                        continue;
                    if (restriktedCombinations[newNode])
                        continue;

                    used[newNode] = true;
                    nextLevelNodes.Enqueue(newNode);
                }
            }
            nodes = nextLevelNodes;
        }

        return -1;
    }
}