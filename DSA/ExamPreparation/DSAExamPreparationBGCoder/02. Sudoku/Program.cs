using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.Sudoku
{
    class Program
    {
        static char[,] matrix = new char[9, 9];
        static void Main(string[] args)
        {
            int n = 9;
           
            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = line[j];
                }
            }
            Solve(0, 0);
            Print(matrix);
        }

        private static void Print(char[,] matrix)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(matrix[i,j]);
                }
                Console.WriteLine();
            }
        }

        private static void Solve(int row, int col)
        {
            for (int i = row; i < 9; i++)
            {
                for (int j = col; j < 9; j++)
                {
                    if (matrix[i,j]=='-')
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            matrix[i, j] = (char)num;
                            Solve(i, j);
                        }
                    }
                }
            }
        }
    }
}
