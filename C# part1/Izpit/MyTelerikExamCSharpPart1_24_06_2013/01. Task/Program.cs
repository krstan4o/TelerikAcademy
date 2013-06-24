using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        //Input 
        int[] seedArea = new int[6];
        int[] seedAmount = new int[6];
        int totalArea = 250;
        for (int i = 0; i < 10; i++)
        {
            int number = int.Parse(Console.ReadLine());
            if (i % 2 == 0)
            {
                seedAmount[i / 2] = number;
            }
            else
            {
                seedArea[i / 2] = number;
            }
        }
        int beenSeedAmount = int.Parse(Console.ReadLine());
        seedAmount[5] = beenSeedAmount;

        double[] costsPerSeed = new double[]
        {
            0.5,
            0.4,
            0.25,
            0.6,
            0.3,
            0.4
        };

        int beanArea = totalArea - seedArea.Sum();
        seedArea[5] = beanArea;

        // Calculatind costs:
        double totalCosts = 0;
        for (int i = 0; i < seedAmount.Length; i++)
        {
            totalCosts += seedAmount[i] * costsPerSeed[i];
        }
        Console.WriteLine("Total costs: {0:0.00}", totalCosts);
        if (beanArea < 0)
        {
            Console.WriteLine("Insufficient area");
        }
        else if (beanArea == 0)
        {
            Console.WriteLine("No area for beans");
        }
        else
        {
            Console.WriteLine("Beans area: {0}", beanArea);
        }
    }
}

