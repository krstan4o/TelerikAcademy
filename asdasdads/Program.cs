using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdasdads
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    string input = Console.ReadLine();
        //    string[] wtf = input.Split(' ');
        //    short bonbons = short.Parse(wtf[0]);
        //    short a = short.Parse(wtf[1]);
        //    short b = short.Parse(wtf[2]);
        //    int count = 0;
        //    while (bonbons / a > 0 && bonbons / b > 0)
        //    {
        //        if (bonbons % a == 0)
        //        {
        //            bonbons -= (short)(bonbons / a);
        //        }
        //        else if (bonbons % a != 0)
        //        {
        //            short kup4inki = (short)(bonbons / a);
        //            short vremenakup4inka = 0;
        //            while (vremenakup4inka + kup4inki < bonbons)
        //            {
        //                vremenakup4inka += kup4inki;
        //            }
        //            short razlika = (short)(bonbons - vremenakup4inka);
        //            bonbons -= razlika;
        //        }
        //        count++;
        //        if (bonbons % b == 0)
        //        {
        //            bonbons -= (short)(bonbons / b);
        //        }
        //        else if (bonbons % b != 0)
        //        {
        //            short kup4inki = (short)(bonbons / b);
        //            int vremenakup4inka = 0;
        //            while (vremenakup4inka <= bonbons)
        //            {
        //                vremenakup4inka += kup4inki;
        //            }
        //            short razlika = (short)(bonbons - vremenakup4inka);
        //            bonbons -= razlika;
        //        }
        //        count++;
        //    }
        //    Console.WriteLine(++count);
        //}
        static void Main()
        {
            int N = int.Parse(Console.ReadLine());
            string input = Console.ReadLine();
            string[] splited = input.Split(' ');
            int[] arr = new int[N];
            int bestPrime = int.MinValue;
            for (int i = 0; i < N; i++)
            {
                arr[i] = int.Parse(splited[i]);
                int wtf = MaxPrime(arr[i]);
                if (wtf > bestPrime)
                {
                    bestPrime = wtf;
                }
            }

            Console.WriteLine(bestPrime);
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % bestPrime == 0)
                {
                    Console.Write(i + " ");
                }
            }

        }
        static int MaxPrime(int a) 
        {
            int count = 0;
            for (int i = 1; i <= a; i++)
            {
                if (a % i ==0)
                {
                    count++;
                }
                if (count > 2)
                {
                    return -1;
                }
            }
            return a;
        }
    }
}