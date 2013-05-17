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
        Console.Write("Please enter a position of element to check if it's bigger than its\n two neighbors: ");
        int position = int.Parse(Console.ReadLine());

        if (position < 1 || position > array.Length - 2)
        {
            Console.WriteLine("Invalid position...");
            return;
        }
        else
        {
            Console.WriteLine(CheckNeighbors(array, position));
        }
    }

    public static bool CheckNeighbors(int[] arr,int index)
    {
        if (arr[index] > arr[index - 1] && arr[index] > arr[index + 1])
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}

