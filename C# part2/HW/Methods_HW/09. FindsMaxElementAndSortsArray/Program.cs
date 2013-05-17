using System;
using System.Linq;

class Program
{
    static int[] SortArrayAscending(int[] array)
    {
        int[] sorted = SortArrayDescending(array);
        sorted = sorted.Reverse().ToArray();
        return sorted;
    }
    static int[] SortArrayDescending(int[] array)
    {
        int maxElementIndex = 0;
        for (int i = 0; i < array.Length; i++)
        {
            maxElementIndex = GetMaximalElement(array, i);
            int swap = array[maxElementIndex];
            array[maxElementIndex] = array[i];
            array[i] = swap;
        }
        return array;
    }
    static int GetMaximalElement(int[] array, int startIndex)
    {
        int maxElement = startIndex;
        for (int i = startIndex; i < array.Length; i++)
        {
            if (array[maxElement] < array[i])
            {
                maxElement = i;
            }
        }
        return maxElement;
    }
    static void PrintArray(int[] array)
    {
        Console.WriteLine();
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write("{0} ", array[i]);
        }
        Console.WriteLine();
    }
    static void Main()
    {
        int[] array = { 5, 62, 37, 44, 77, 9, 11, 102, 13, 22, 36, 17 };
        PrintArray(array);
        
        Console.WriteLine(array[GetMaximalElement(array,0)]);
        int[] sortedArray = SortArrayAscending(array);
        PrintArray(sortedArray);
        sortedArray = SortArrayDescending(array);
        PrintArray(sortedArray);
        Console.WriteLine();
    }
}