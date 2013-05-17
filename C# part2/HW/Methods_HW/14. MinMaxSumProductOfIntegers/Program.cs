using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter sequence of integers separeted by \" \":");
        string str = Console.ReadLine();
        string[] str2 = str.Split();
        List<int> list = new List<int>();
        foreach (var item in str2)
        {
            list.Add(int.Parse(item));
        }
        
      
        Console.WriteLine("The Minimum of the set of integers is: {0}",Min(list));
        Console.WriteLine("The Maximum of the set of integers is: {0}", Max(list));
        Console.WriteLine("The Average of the set of integers is: {0}", Avg(list));
        Console.WriteLine("The Sum of the set of integers is: {0}", Sum(list));
        Console.WriteLine("The Product of the set of integers is: {0}", Product(list));
    }

    static long Product(List<int> list)
    {
        long product = 1;
        for (int i = 0; i < list.Count; i++)
        {
            product *= list[i];
        }
        return product;
    }

    static decimal Avg(List<int> list)
    {
        return (decimal)Sum(list) / list.Count;
    }

    static int Sum(List<int> list)
    {
        int sum = 0;
        for (int i = 0; i < list.Count; i++)
        {
            sum += list[i];
        }
        return sum;
    }

    static int Max(List<int> list)
    {
        int currentMaxElement = int.MinValue;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i]>currentMaxElement)
            {
                currentMaxElement = list[i];
               
            }
        }
        return currentMaxElement;
    }

    static int Min(List<int> list)
    {
        int currentMinElement = int.MaxValue;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] < currentMinElement)
            {
                currentMinElement = list[i];
            }
        }
        return currentMinElement;
    }
}

