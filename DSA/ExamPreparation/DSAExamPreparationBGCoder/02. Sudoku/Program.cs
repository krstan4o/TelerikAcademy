using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.Sudoku
{
    class Program
    {
        static int[,] matrix = new int[9, 9];

        static void Main(string[] args)
        {
            int n = 9;
           
            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();
                for (int j = 0; j < n; j++)
                {
                    if (line[j] == '-')
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        matrix[i, j] = int.Parse(line[j].ToString());
                    }
                }
            }
            Solve(0, 0);
        }

        private static void Print(int[,] matrix)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
            int debug = 2;
        }

        private static void Solve(int row, int col)
        {
            if (row == 9 && col == 0)
            {
                Print(matrix);
                Environment.Exit(0);
            }
            else if (matrix[row, col] == 0)
            {
                for (int num = 1; num <= 9; num++)
                {
                    if (CheckRow(row, num) || CheckCol(col, num) || CheckSquare(row, col, num))
                    {
                        continue;
                    }

                    matrix[row, col] = num;
                    Solve(NextRow(row, col), NextCol(col));
                    matrix[row, col] = 0;
                }
            }
            else
            {
                Solve(NextRow(row, col), NextCol(col));
            }
        }

        private static bool CheckRow(int row, int num) 
        {
            for (int i = 0; i < 9; i++)
            {
                if (matrix[row, i] == num)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CheckSquare(int row, int col, int num) 
        {
            int startRow = (row / 3) * 3;
            int startCol = (col / 3) * 3;
            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startCol; j < startCol + 3; j++)
                {
                    if (matrix[i, j] == num)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool CheckCol(int col, int num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (matrix[i, col] == num)
                {
                    return true;
                }
            }
            return false;
        }

        private static int NextRow(int row, int col)
        {
            col++;
            if (col > 8)
            {
                return row + 1;
            }
            return row;
        }

        private static int NextCol(int col)
        {
            col++;
            if (col > 8)
            {
                return 0;
            }
            return col;
        }
    }
}
