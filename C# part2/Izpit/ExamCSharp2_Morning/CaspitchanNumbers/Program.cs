using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

class Program
{
    private static Dictionary<BigInteger, string> table;
    private static Stack<BigInteger> stack;

    static void Main()
    {
        table = new Dictionary<BigInteger, string>(256);
        PopulateTable();
        stack = new Stack<BigInteger>();

        BigInteger input = BigInteger.Parse(Console.ReadLine());
        stack.Push(input);

        if (table.ContainsKey(input))
        {
            Console.WriteLine(table[input]);
        }
        else
        {
            var result = new StringBuilder();
            if (table.ContainsKey(input / 256))
            {
                result.Append(table[input / 256]);
                result.Append(table[input % 256]);
                Console.WriteLine(result);
            }
            else
            {
                result.Clear();
                while (input > 255)
                {
                    input = input / 256;
                    stack.Push(input);
                }
                while (stack.Count > 0)
                {
                    if (table.ContainsKey(stack.Peek() % 256))
                    {
                        result.Append(table[stack.Pop() % 256]);
                    }
                }

                Console.WriteLine(result);
            }
        }
    }

    private static void PopulateTable()
    {
        string bigEnglish = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string smallEnglish = "abcdefghi";
        int index = 0;
        for (int i = 0; i < bigEnglish.Length; i++)
        {
            table.Add(index, bigEnglish[i].ToString());
            index++;
        }
            
        for (int i = 0; i < smallEnglish.Length; i++)
        {
            for (int j = 0; j < bigEnglish.Length; j++)
            {
                if (index >= 256)
                {
                    break;
                }
                   
                table.Add(index, smallEnglish[i].ToString() + bigEnglish[j]);
                index++;
            }
        }
    }
}
