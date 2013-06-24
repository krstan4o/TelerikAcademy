using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.Task_Bittris
{
    class Program
    {
        static bool[,] matrix = new bool[4, 8];
        static void Main(string[] args)
        {
            
            int n = int.Parse(Console.ReadLine());
             int currentRow=0;
            int currentCol=0;
            for (int i = 0; i < n; i++)
            {
                string inputCommand = Console.ReadLine().Trim();
                if (char.IsDigit(inputCommand[0]))
                {
                    int number = int.Parse(inputCommand);
                    string numberToBinary = Convert.ToString(number, 2);

                    while (numberToBinary.Length < 8)
                    {
                        string withTrailingZeroes = "0" + numberToBinary;
                        numberToBinary = withTrailingZeroes;
                    }
                    for (int j = 0; j < numberToBinary.Length; j++)
                    {
                        if (numberToBinary[j] == '1')
                        {
                            matrix[0, j] = true;
                        }
                    }
                }
                else if (inputCommand[0] == 'D')
                {
                    CheckIsEmptyNextRow(currentRow + 1);
                    currentRow = DownCommand(currentRow);
                }
                else if (inputCommand[0] == 'R')
                {
                    RightCommand(currentRow);
                }
                else if (inputCommand[0] == 'L')
                {
                    LeftCommand(currentRow);
                }
              
            }
        }

        private static void CheckIsEmptyNextRow(int currentRow)
        {
           
        }

        private static void LeftCommand(int row)
        {
            if (!matrix[row, 0])
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                {

                    if (matrix[row, i])
                    {
                        matrix[row, i] = false;
                        matrix[row, i - 1] = true;
                    }
                }
            }
        }
        private static void RightCommand(int row)
        {
            if (!matrix[row, matrix.GetLength(1) - 1])
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                {

                    if (matrix[row, i])
                    {
                        matrix[row, i] = false;
                        matrix[row, i + 1] = true;
                    }
                }
            }
        }

        private static int DownCommand(int row)
        {
            if (row < matrix.GetLength(0)-1)
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    if (matrix[row, i])
                    {
                        matrix[row, i] = false;
                        matrix[row + 1, i] = true;
                    }
                }
            }
            return row++;
        }

       
    }
}

