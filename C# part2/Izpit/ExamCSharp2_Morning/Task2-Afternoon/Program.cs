using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2_Afternoon
{

    class Program
    {
        static StringBuilder extractedLetters;
        static Dictionary<char, int> alphabetLettersToPossitions;
        private const string englishAlphabet = "abcdefghijklmnopqrstuvwxyz";

        static void Main()
        {
            extractedLetters = new StringBuilder();
            alphabetLettersToPossitions = new Dictionary<char, int>(52);

            string[] wordsInput = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            PopulateDictonary();
            ExtractCharacters(wordsInput);
            MoveLetters();
            Console.WriteLine(extractedLetters);

        }

        private static void ExtractCharacters(string[] words)
        {

            int sum = words.Max(x => x.Length);
          
            int index = 0;

            while (sum > 0)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length - 1 >= index)
                    {
                        extractedLetters.Append(words[i][words[i].Length - 1 - index]);
                      
                    }

                }
                sum--;
                index++;
            }


        }

        private static void PopulateDictonary()
        {
            for (int i = 1; i <= englishAlphabet.Length; i++)
            {
                alphabetLettersToPossitions.Add(englishAlphabet[i - 1], i);
                alphabetLettersToPossitions.Add(char.ToUpper(englishAlphabet[i - 1]), i);
            }

        }

        private static void MoveLetters()
        {
            int length = extractedLetters.Length;
            for (int i = 0; i < length; i++)
            {
        
                int alphabetNumber = alphabetLettersToPossitions[extractedLetters[i]];

                char element = extractedLetters[i];
                int newIndex = (alphabetNumber + i) % length;

                if (newIndex <= i)
                {

                    extractedLetters.Insert(newIndex, element);
                    extractedLetters.Remove(i + 1, 1);
                }
                else
                {

                    extractedLetters.Insert(newIndex + 1, element);
                    extractedLetters.Remove(i, 1);
                }
            }

        }
    }
}