using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.Task_TheSecretNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine().Trim();
            
            int[] diggits = new int[input.Length + 1];
           
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    int diggit = int.Parse(input[i].ToString());
                    diggits[i + 1] = diggit;
                }
               
               
            }
            int specialSum = 0;
            int position = 0;
            for (int i = diggits.Length - 1; i >= 1; i--)
            {
                position++;
                if (position % 2 != 0)
                {
                    specialSum += diggits[i] * (position * position);
                }
                else
                {
                    specialSum += (diggits[i] * diggits[i]) * position;
                }
            }
            int R = specialSum % 26;
            int start = R + 1;
            int length = specialSum % 10;
            if (length <= 0)
            {
                Console.WriteLine(specialSum);
                Console.WriteLine(input + " has no secret alpha-sequence");
            }
            else
            {
                Console.WriteLine(specialSum);
                Dictionary<int, char> letters = new Dictionary<int, char>();
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                for (int i = 0; i < alphabet.Length; i++)
                {
                    letters.Add(i + 1, alphabet[i]);
                }

                string result = "";

                while (length > 0)
                {
                    if (letters.ContainsKey(start))
                    {
                        result += letters[start];
                        length--;
                        start++;
                    }
                    else
                    {
                        start = 1;
                    }
                }
                Console.WriteLine(result);
            }
        }
    }
}
