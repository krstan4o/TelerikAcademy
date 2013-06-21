using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter a word:");
        string word = Console.ReadLine();
        word.ToLower();
        string englishAlphabet = "abcdefghijklmnopqrstuvwhyz";
        char[] array = englishAlphabet.ToCharArray();
        char[] array1 = word.ToCharArray();
        Console.WriteLine("The index of all letters in array from the word is:");
        for (int i = 0; i < array1.Length; i++)
        {
            for (int j = 0; j < array.Length; j++)
            {
                if (array1[i]==array[j])
                {
                    Console.WriteLine("{0} {1}",array1[i],j);
                }
            }
        }
    }
}

