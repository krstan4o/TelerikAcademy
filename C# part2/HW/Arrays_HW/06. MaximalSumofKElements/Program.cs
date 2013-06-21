using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter array size...");
        int N = int.Parse(Console.ReadLine());
        Console.WriteLine("Please enter a integer K...");
        int K = int.Parse(Console.ReadLine());
        int sum = 0;
        int[] arr = new int[N];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = int.Parse(Console.ReadLine());
        }

        Array.Sort(arr);

        for (int i = N-1; i >= N-K; i--)
        {
            sum += arr[i];
        }
        Console.WriteLine(sum);
    }
}

