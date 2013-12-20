using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static List<string> input;

        static void Main()
        {
            int count = int.Parse(Console.ReadLine());
            input = new List<string>(count);

            int maxWordLen = int.MinValue;

            for (int i = 0; i < count; i++)
            {
                input.Add(Console.ReadLine());
                if (input[i].Length > maxWordLen)
                {
                    maxWordLen = input[i].Length;
                }
            }
            Reorder();
            Print(maxWordLen);
        }

        private static void Reorder()
        {
            for (int i = 0; i < input.Count; i++)
            {
                int newPosition = input[i].Length % (input.Count + 1);

                input.Insert(newPosition, input[i]);

                if (newPosition < i)
                {
                    input.RemoveAt(i + 1);
                }
                else
                {
                    input.RemoveAt(i);
                }
            }
        }

        private static void Print(int maxWordLen)
        {
            //int index = 0;
            //while (index <= maxWordLen - 1)
            //{
            //    for (int i = 0; i < input.Count; i++)
            //    {
            //        if (index <= input[i].Length - 1)
            //        {
            //            Console.Write(input[i][index]);
            //        }
            //    }
            //    index++;
            //}
            StringBuilder result = new StringBuilder();
 
            string currentWord = string.Empty;
            int counter = 0;
            while (maxWordLen > 0)
            {
                for (int i = 0; i < input.Count; i++)
                {
                    currentWord = input[i];
                    if (currentWord.Length <= counter)
                    {
                        continue;
                    }
                    result.Append(currentWord[counter]);
                }
                counter++;
                maxWordLen--;
            }
 
            Console.WriteLine(result.ToString());
        }
    }
}
