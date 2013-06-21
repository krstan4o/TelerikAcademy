using System;

class Program
{
    static void Main()
    {
        int[] array = new int[20];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i * 5;
        }
        Console.WriteLine("The elements of the array multiplied by 5 are:");
        foreach (var item in array)
        {
            Console.WriteLine(item);
        }
    }
}
