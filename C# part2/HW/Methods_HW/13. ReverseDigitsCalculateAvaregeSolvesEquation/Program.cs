using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("1-Reverse digits of number.");
        Console.WriteLine("2-Calculates the average of a sequence of integers.");
        Console.WriteLine("3-Solves a linear equation a * x + b = 0");
        Console.Write("Your choise: ");
        byte choise = byte.Parse(Console.ReadLine());
        switch (choise)
        {
            case 1: Console.WriteLine("Please enter a number:");
                decimal number = decimal.Parse(Console.ReadLine());
                if (number<0)
                {
                    Console.WriteLine("Please enter positive number...");
                    return;
                }
                Console.WriteLine(ReverseDigits(number));
                break;
            case 2: Console.WriteLine("Please enter number of integers:");
                try
                {
                    string str = Console.ReadLine();
                    int size = int.Parse(str);
                    int[] array = new int[size];
                    Console.WriteLine("Now enter {0} numbers", size);
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = int.Parse(Console.ReadLine());
                    }
                    Console.WriteLine("The avarege sum is: {0}", AverageOfIntegers(array));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number...");
                }
                break;
            case 3: Console.WriteLine("Please enter a and b:");
                decimal a = decimal.Parse(Console.ReadLine());
                if (a==0)
                {
                    Console.WriteLine("Please enter a!=0");
                    return;
                }
                decimal b = decimal.Parse(Console.ReadLine());
                Console.WriteLine("x = {0}",LinerEquation(a,b));
                break;
            default: Console.WriteLine("Invalid choise.");
                break;
        }
    }

    static decimal LinerEquation(decimal a,decimal b)
    {
        
        return -(b)/a;
    }

     static float AverageOfIntegers(int[] arr)
    {
        int sum = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
        }
        return sum/arr.Length;
    }
    static string ReverseDigits(decimal num)
    {
         string numToString= num.ToString();
         char[] numToChar = numToString.ToCharArray();
         char temp;
         for (int i = 0; i < numToChar.Length/2; i++)
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

