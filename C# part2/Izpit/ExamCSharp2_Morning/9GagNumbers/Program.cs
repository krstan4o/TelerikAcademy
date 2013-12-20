using System;
using System.Collections.Generic;
using System.Numerics;

class Program
{
    private static string input;
    private static Dictionary<string, int> table;
    private static Stack<BigInteger> stack;

    static void Main()
    {
        input = Console.ReadLine();
        PopulateTable();
        stack = new Stack<BigInteger>();

        Console.WriteLine(Solve());
    }

    private static BigInteger Solve()
    {
        
        for (int i = 0; i < input.Length; i++)
        {
            int length = 2;
            string element = input.Substring(i, length);

            if (!table.ContainsKey(element))
            {
                while (!table.ContainsKey(element))
                {
                    length++;
                    element = input.Substring(i, length);
                }
                i += length - 1;
                stack.Push(table[element]);
            }
            else
            {
                stack.Push(table[element]);
                i++;
            }
        }
        
        BigInteger result = stack.Pop();
        int count = stack.Count;
        BigInteger nineStepen = 1;
        for (int i = 0; i < count; i++)
        {                   
                nineStepen *= 9;
                result += stack.Pop() * nineStepen;
        }

        return result;
    }

    private static void PopulateTable() 
    {
        table = new Dictionary<string, int>(9);
        table.Add("-!", 0);
        table.Add("**", 1);
        table.Add("!!!", 2);
        table.Add("&&", 3);
        table.Add("&-", 4);
        table.Add("!-", 5);
        table.Add("*!!!", 6);
        table.Add("&*!", 7);
        table.Add("!!**!-", 8);
    }
}

