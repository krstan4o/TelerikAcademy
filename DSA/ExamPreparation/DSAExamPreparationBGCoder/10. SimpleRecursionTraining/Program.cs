using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
   

    private static void Print(int[] vector)
    {
        for (int i = 0; i <vector.Length; i++)
        {
            Console.Write(vector[i] + " ");
        }
        Console.WriteLine();
    }
    static void Main()
    {
        
        int n = int.Parse(Console.ReadLine());
        int[] vector = new int[n];
        Gen01(0, vector,4);
    }

    private static void Gen01(int index, int[] vector,int start)
    {
        if (index == vector.Length)
        {
            Print(vector);
        }
        else
        {
            for (int i = start; i <=8; i++)
            {
                vector[index] = i;
                Gen01(index + 1, vector, i+1);
            }
        }
    }

    
}

