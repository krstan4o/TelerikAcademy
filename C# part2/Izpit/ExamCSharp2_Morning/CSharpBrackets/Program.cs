using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Program
    {
        private static string separator;
        private static int rowsInput;
        private static string[] input;
        static void Main()
        {
            rowsInput = int.Parse(Console.ReadLine());
            separator = Console.ReadLine();
            input = new string[rowsInput];
            for (int i = 0; i < rowsInput; i++)
            {
                input[i] = Console.ReadLine();
            }

            var debug = 2;
        }
    }

