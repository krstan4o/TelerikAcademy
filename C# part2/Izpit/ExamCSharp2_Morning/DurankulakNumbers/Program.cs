using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

    class Program
    {
        private static Dictionary<string, int> table;
        private static string input;
        private static Stack<int> stack;
        static void Main()
        {
            table = new Dictionary<string, int>();
            stack = new Stack<int>();
            PopulateTable();
            input = Console.ReadLine();

            Console.WriteLine(Solve()); 
        }

        private static BigInteger Solve()
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLower(input[i]))
                {
                    stack.Push(table[input[i].ToString() + input[i + 1]]);
                    i++;
                }
                else
                {
                    stack.Push(table[input[i].ToString()]);
                }
            }

            BigInteger result = 0;
            result += stack.Pop();
            int count = 1;
            while (stack.Count > 0)
            {
                result += stack.Pop() * (BigInteger)Math.Pow((double)168, count);
                count++;
              
            }
            return result;

        }

        private static void PopulateTable()
        {
            string bigEnglish = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string smallEnglish = "abcdef";
            int index = 0;
            for (int i = 0; i < bigEnglish.Length; i++)
            {
                table.Add(bigEnglish[i].ToString(), index);
                index++;
            }

            for (int i = 0; i < smallEnglish.Length; i++)
            {
                for (int j = 0; j < bigEnglish.Length; j++)
                {
                    if (index >= 168)
                    {
                        break;
                    }

                    table.Add(smallEnglish[i].ToString() + bigEnglish[j], index);
                    index++;
                }
            }
        }
    }

