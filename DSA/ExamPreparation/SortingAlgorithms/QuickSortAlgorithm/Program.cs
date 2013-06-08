using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] arr = { -5,-6 };
        QuickSort.sort(arr, 0, arr.Length-1);
        foreach (var item in arr)
        {
            Console.WriteLine(item);
        }
    }
}