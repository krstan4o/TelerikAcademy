using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    private static StringBuilder sb;
    static void Main()
    {
        sb = new StringBuilder();
        int rows = int.Parse(Console.ReadLine());
        int charsInRow = int.Parse(Console.ReadLine());

        string[] inputText = new string[rows];
        for (int i = 0; i < rows; i++)
        {
            inputText[i] = Console.ReadLine();
        }

        for (int i = 0; i < inputText.Length; i++)
        {
            string[] wordsInCurrentRow = inputText[i].Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            int curRowCharsCount = 0;
            for (int j = 0; j < wordsInCurrentRow.Length; j++)
            {
                if (wordsInCurrentRow[j].Length + 1 + curRowCharsCount <= charsInRow)
                {
                    sb.Append(wordsInCurrentRow[j] + ' ');
                    curRowCharsCount += wordsInCurrentRow.Length + 1;
                }
            }
            sb.AppendLine();
            
        }




        Console.WriteLine(sb);
    }
}

