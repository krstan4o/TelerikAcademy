using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
class Program
{
    static void Main()
    {
        string[] pleasantness = Console.ReadLine().Split(new char[] {',',' ' },StringSplitOptions.RemoveEmptyEntries);
        int variety = int.Parse(Console.ReadLine());
        int[] numbers = new int[pleasantness.Length];
        for (int i = 0; i < pleasantness.Length; i++)
        {
            numbers[i] = int.Parse(pleasantness[i]);
        }
        int amplitude = 200;
        List<int> solvedTasks = new List<int>();
        solvedTasks.Add(numbers[0]);
        for (int i = 1; i < numbers.Length - 2; i++)
        {
            int min = solvedTasks.Min();
            int max = solvedTasks.Max();
            if (solvedTasks[i] > max)
            {
                
            }
        }
        
    }
}

