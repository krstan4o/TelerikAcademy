using System;
using System.Collections.Generic;

class Program
{
static void Main(string[] args)
        {
            //int[] array = {1,0,4,3,-9,3,4,-7 };
 
           
            Console.Write("Enter the lenght of array: ");
 
            int n = int.Parse(Console.ReadLine());
 
            int[] array = new int[n];
 
            Console.WriteLine("Enter members of array: ");
 
           
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("array[{0}]=", i);
                array[i] = int.Parse(Console.ReadLine());
 
            }
           
 
            List<int> copyOfArray = new List<int>();
            List<int> sortedCopyOfArray = new List<int>();
 
            for (int i = 0; i < array.Length; i++)
            {
 
                copyOfArray.Add(array[i]);
 
 
            }
 
            sortedCopyOfArray = quickSort(copyOfArray);
 
            Console.Write("Sorted array is : {" + sortedCopyOfArray[0]);
 
            for (int i = 1; i < sortedCopyOfArray.Count; i++)
            {
                Console.Write(", {0}", sortedCopyOfArray[i]);
 
            }
            Console.WriteLine("}");
 
        }
 
 
    
     static List<int> Concatenate(List<int> a, List<int> pivot, List<int> b)
     {
         List<int> result = new List<int>();

         for (int i = 0; i < a.Count; i++)
         {
             result.Add(a[i]);
         }

         for (int i = 0; i < pivot.Count; i++)
         {
             result.Add(pivot[i]);
         }



         for (int i = 0; i < b.Count; i++)
         {
             result.Add(b[i]);
         }

         return result;

     }


     //////////////////////////////////////////////////////////////////////////////////////////////////

     static List<int> quickSort(List<int> unsortedArray)
     {
         List<int> sortedArray = new List<int>();
         List<int> minArray = new List<int>();
         List<int> maxArray = new List<int>();
         List<int> pivotArray = new List<int>();

         int pivot = (unsortedArray.Count / 2) - 1;

         if (unsortedArray.Count <= 1)
         {
             return unsortedArray;
         }

         for (int i = 0; i < unsortedArray.Count; i++)
         {
             if (unsortedArray[i] < unsortedArray[pivot])
             {
                 minArray.Add(unsortedArray[i]);
             }
             else if (unsortedArray[i] > unsortedArray[pivot])
             {
                 maxArray.Add(unsortedArray[i]);
             }

             else if (unsortedArray[i] == unsortedArray[pivot])
             {
                 pivotArray.Add(unsortedArray[i]);
             }

         }


         return Concatenate(quickSort(minArray), pivotArray, quickSort(maxArray));
     }
}
        
    

