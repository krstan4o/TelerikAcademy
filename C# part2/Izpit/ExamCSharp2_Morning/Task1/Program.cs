using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        static void Main()
        {
            Regex regex = new Regex(@"(.{4})");
            string[] input = regex.Split(Console.ReadLine()).Where(s => !string.IsNullOrEmpty(s)).ToArray();

            Dictionary<string, int> table = new Dictionary<string, int>();

            table.Add("Rawr", 0);
            table.Add("Rrrr", 1);
            table.Add("Hsst", 2);
            table.Add("Ssst", 3);
            table.Add("Grrr", 4);
            table.Add("Rarr", 5);
            table.Add("Mrrr", 6);
            table.Add("Psst", 7);
            table.Add("Uaah", 8);
            table.Add("Uaha", 9);
            table.Add("Zzzz", 10);
            table.Add("Bauu", 11);
            table.Add("Djav", 12);
            table.Add("Myau", 13);
            table.Add("Gruh", 14);

            Stack<int> numbers = new Stack<int>();
            for (int i = 0; i < input.Length; i++)
            {
                if (table.ContainsKey(input[i]))
                {
                    numbers.Push(table[input[i]]);
                }
            }
            long sum = numbers.Pop();
            int count = numbers.Count;
            for (int i = 1; i <= count; i++)
            {
                sum += numbers.Pop() * (long)Math.Pow(15, i);
            }
            Console.WriteLine(sum);
        }
    }
}
