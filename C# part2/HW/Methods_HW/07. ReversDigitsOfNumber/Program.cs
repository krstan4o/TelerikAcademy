using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter number: ");
        Console.WriteLine("The reversed number is: {0}\n",ReverseDigits(decimal.Parse(Console.ReadLine())));
    }
    static string ReverseDigits(decimal num)
    {
        string numToString = num.ToString();
        char[] numToChar = numToString.ToCharArray();
        char temp;
        for (int i = 0; i < numToChar.Length / 2; i++)
        {
            temp = numToChar[i];
            numToChar[i] = numToChar[(numToChar.Length - 1) - i];
            numToChar[(numToChar.Length - 1 - i)] = temp;
        }
        numToString = string.Empty;
        for (int i = 0; i < numToChar.Length; i++)
        {
            numToString += numToChar[i];
        }

        return numToString;
    }
}

