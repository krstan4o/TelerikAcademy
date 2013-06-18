using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] input = Console.ReadLine().Split();
            //int countForFreePackage = int.Parse(input[0]);
            //int a = countForFreePackage;
            //int buyKonPriceOfKMinusOne = int.Parse(input[1]);
            //int b = buyKonPriceOfKMinusOne;
            //int moneyToSpend = int.Parse(input[2]);
            //int priceOfOnePackage = int.Parse(input[3]);

            //int buyedPackages = 0;
          
            //while (moneyToSpend >= priceOfOnePackage)
            //{
               

               
            //    if (buyedPackages == buyKonPriceOfKMinusOne)
            //    {
            //        moneyToSpend += priceOfOnePackage;
            //        buyKonPriceOfKMinusOne += b;
                   
            //    }
            //    if (buyedPackages == countForFreePackage)
            //    {
                   
            //        buyedPackages++;
            //        countForFreePackage += a;
                   
                  
            //    }

            //    buyedPackages++;
            //    moneyToSpend -= priceOfOnePackage;

              
                

            //}
            //Console.WriteLine(buyedPackages);
            int N = int.Parse(Console.ReadLine());

            string[] input = Console.ReadLine().Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries);
            List<int> list = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                list.Add(int.Parse(input[i]));
            }
            list.Sort();
            int secounds = 0;
            for (int i = 0; i < list.Count-1; i++)
            {
                secounds += list[i];
            }
            Console.WriteLine(secounds);
        }
    }
}
