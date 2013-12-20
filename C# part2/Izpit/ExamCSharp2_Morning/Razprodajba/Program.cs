using System;
using System.Collections.Generic;
using System.Linq;

    class Program
    {
        static void Main()
        {
            string[] input = Console.ReadLine().Split();

            int bonusPlusOnePackage = int.Parse(input[0]);
            int bonusDiscount = int.Parse(input[1]);
            int money = int.Parse(input[2]);
            int packagePrice = int.Parse(input[3]);

            int bonusPackages = 0;
            int buyedPackages = 0;
            while (money >= packagePrice)
            {
                buyedPackages++;

                if (buyedPackages % bonusPlusOnePackage == 0)
                {
                    bonusPackages++;
                }

                if (buyedPackages % bonusDiscount == 0)
                {
                   
                }
                else
                {
                    money -= packagePrice;
                }
            }
            Console.WriteLine(buyedPackages);
        }
    }

