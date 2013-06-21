using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter the element u want to search for...");
        int element = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter array size:");
        int size = int.Parse(Console.ReadLine());
        int[] array = new int[size];
        Console.WriteLine("Now enter array elements.");
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = int.Parse(Console.ReadLine());
        }

        Array.Sort(array);
        Console.WriteLine("\nThe new sorted array is:\n");
        foreach (var item in array)
        {
            Console.WriteLine(item);
        }
        
        int index1 = Array.BinarySearch(array, element);
        Console.WriteLine("The {0} element is with index: {1}.", element, index1);
        
    }
}

