using System;
using System.Linq;
using System.Diagnostics;

namespace DataStructuresAlgorithmsComplexity_HW
{
    class Task1
    
    {
        public static int count = 0;
        static void Main(string[] args)
        {
            //What is the expected running time of the following C# code? Explain why.
            //Assume the array's size is n.
        }

        long Compute(int[] arr)
        {
            long count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                int start = 0, end = arr.Length - 1;
                while (start < end)
                    if (arr[start] < arr[end])
                    {
                        start++;
                        count++;
                    }
                    else
                        end--;
            }
            return count;
            //The algorithm complexity is: O(n*n)- where n is arr.Length
            //Becouse we have 1 for cicle and one while in the for cickle
            //When n is 100 000 it takes ~ 1-2 minutes.
        }

        
    }
}

