using System;

public class Program
{
    static void Main()
    {
        Console.Write("Please enter array size: ");
        int size = int.Parse(Console.ReadLine());
        int[] array = new int[size];
        Console.WriteLine("Now enter array's elements");
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = int.Parse(Console.ReadLine());
        }
        Console.Write("Please enter a number to search for in the given array: ");
        int number=int.Parse(Console.ReadLine());
        Console.WriteLine("The number: {0} apears {1} times in the array.",number,CountNumber(array,number));

    }
    public static int CountNumber(int[] arr,int searchNumber)
    {
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i]==searchNumber)
            {
                count++;
            }
        }
        return count;
    }
}

