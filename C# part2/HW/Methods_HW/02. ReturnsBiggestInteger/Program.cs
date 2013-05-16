using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter two numbers: ");
        int number1 = int.Parse(Console.ReadLine());
        int number2 = int.Parse(Console.ReadLine());
        Console.WriteLine("The biggest number is: {0}",GetMax(number1, number2));
        Console.WriteLine("Now enter third number:");
        int number3 = int.Parse(Console.ReadLine());
        Console.WriteLine("The biggest number is: {0}", GetMax(GetMax(number1,number2), number3));
    }

 
    static int GetMax(int number1,int number2)
    {
        return Math.Max(number1, number2);        
    }
    
}

