using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.RecoverMessage
{
    class Program
    {
        static string[] words;
        static List<string> result = new List<string>();
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            words = new string[n];
            for (int i = 0; i < n; i++)
            {
                words[i] = Console.ReadLine();
            }
           
            Solver(0, 0, "");
            int debug = 2;
        }

     

        //
        private static void Solver(int wordIndex, int letterIndex, string currentWord)
        {


            for (int i = 0; i < words.Length; i++)
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    currentWord += words[i][j];
                    Solver(i, j, currentWord);
                }
            }
        }
    }
}
