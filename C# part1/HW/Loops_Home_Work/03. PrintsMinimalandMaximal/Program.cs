using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter a number");
        int n = int.Parse(Console.ReadLine());
        if (n <= 0)
        {
            Console.WriteLine("Please enter a number > 0");
            return;
        }
        int[] arr = new int[n];
        int min = int.MaxValue;
        int max = int.MinValue;
        Console.WriteLine("Now enter numbers equal to the entered number...");
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = int.Parse(Console.ReadLine());
            
            if (arr[i] > max)
            {
                max = arr[i];
            }
            if (arr[i] < min)
           {
               min = arr[i];
           }
        }
        Console.WriteLine("Minimal number is: {0}\nMaximal number is: {1}",min,max);
    }
}