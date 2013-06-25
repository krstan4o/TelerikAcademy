using System;
using System.Linq;


namespace _01.Task_
{
    class Program
    {
        static void Main()
        {
            int framesCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < framesCount; i++)
            {
                string[] line = Console.ReadLine().Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);


            }
        }
    }
    class Frame 
    {
        public string First { get; set; }
        public string Secound { get; set; }
        public Frame(string first, string secound) 
        {
            
        }
    }
}
