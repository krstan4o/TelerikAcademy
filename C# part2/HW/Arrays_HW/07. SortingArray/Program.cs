using System;

class Program
{
    static void Main()
    {
        Console.Write("Please enter array size:");
        int size = int.Parse(Console.ReadLine());
        int[] array = new int[size];
        int smalest = int.MaxValue;
        int index = 0;
        Console.WriteLine("Now enter {0} elements",size);
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i <array.Length; i++)
        {
                 for (int j = i; j < array.Length; j++)
                 {
                  if (array[j]<=smalest)
                     {
                         smalest = array[j];
                         index = j;
                     }
                 }
                 array[index] = array[i];
                 array[i] =smalest;
                 smalest = int.MaxValue;     
        }
        Console.WriteLine("The new Sorted Array is:");
        foreach (var item in array)
        {
            Console.WriteLine(item);
        }
    }
}

