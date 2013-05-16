using System;

class Program1
{
    static void Main(string[] args)
    {
        Console.Write("Please enter array size: ");
        int size = int.Parse(Console.ReadLine());
        
        int[] array = new int[size];
        Console.WriteLine("Now enter array's elements");
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = int.Parse(Console.ReadLine());
        }
        Console.WriteLine("The first element in the array,\nthat is bigger than his neighbors is: {0}",array[ChecksIndex(array)]);
    }
    static int ChecksIndex(int[] arr)
    {
        for (int i = 1; i < arr.Length-1; i++)
        {
            if (Program.CheckNeighbors(arr,i))
            {
                return i;
            }
        }
        return -1;
    }
}
