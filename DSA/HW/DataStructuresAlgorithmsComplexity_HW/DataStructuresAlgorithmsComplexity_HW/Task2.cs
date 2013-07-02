using System;
using System.Linq;

    class Task2
    {
        //What is the expected running time of the following C# code? Explain why.

        long CalcCount(int[,] matrix)
        {
            long count = 0;
            for (int row = 0; row < matrix.GetLength(0); row++)
                if (matrix[row, 0] % 2 == 0)
                    for (int col = 0; col < matrix.GetLength(1); col++)
                        if (matrix[row, col] > 0)
                            count++;
            return count;
            //The algorithm complexity is: O(n*m). Becouse we have 2 for cicles one for the rows and one for the cols this is 
            //in the worst case scanario in the best case is O(n), where n is number of rows,m is number of cols in matrix.
            //Example: 
            //   if n = 100000 and m = 100000 in the worst case where every: matrix[row, 0] % 2 == 0 => the program will work~ 3-4 mins.
        }

    }

