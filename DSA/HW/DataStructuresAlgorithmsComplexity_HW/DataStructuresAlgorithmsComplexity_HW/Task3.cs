using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAlgorithmsComplexity_HW
{
    class Task3
    {
        //* What is the expected running time of the following C# code? Explain why.

        long CalcSum(int[,] matrix, int row)
        {
            long sum = 0;
            for (int col = 0; col < matrix.GetLength(0); col++) 
                sum += matrix[row, col];
            if (row + 1 < matrix.GetLength(1)) 
                sum += CalcSum(matrix, row + 1);
            return sum;
        }
        //In the worst case the complexity is: O(n) where n is number of rows and cols (rows==cols => matrix)
        //The worst case is if we call the method with CalcSum(matrix, int row=0)
        //In best case is O(const), becouse if we have matrix(100,100) and we call the Method CalcSum(matrix, int row = 99)
        //The method will be called recursivly only 1 time
    }
}
