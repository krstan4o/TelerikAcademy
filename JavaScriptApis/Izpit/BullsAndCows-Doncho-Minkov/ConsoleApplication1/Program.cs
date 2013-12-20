using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<string> list = new List<string>();
            //int[] arr = new int[5];
            //Stack<string> asen4e = new Stack<string>();
            

            //list.Add("Ivan");//0
            //list.Add("Ivan");//1
            //list.Add("Ivan");
            //list.Add("Ivan");
            //list.Add("Ivan");
            //list.Add("Ivan");
            //list.Add("Ivanka");
            //list.Add("Mariika");

            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (list[i][list[i].Length-1] == 'a')
            //    {
            //        Console.WriteLine(list[i]);
            //    }
            //}
            double a=2.5;
            double b = 3.5;
            double c = 5.5;
            Triangle triangle = new Triangle(a, b, c);

            triangle.Show();
            Console.WriteLine(triangle.ValidateTriangle());
            Console.WriteLine(triangle.Face());
        }
    }
}
