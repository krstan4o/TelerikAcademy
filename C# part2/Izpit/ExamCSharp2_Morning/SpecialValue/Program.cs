using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static int[][] numbers;
    private static bool[][] visitedPositions;
    private static int numberOfArrays;
    private static int maxPath = 0;
    private static int maxSumNegative = 0;

    static void Main()
    {
        numberOfArrays = int.Parse(Console.ReadLine());
        numbers = new int[numberOfArrays][];
        visitedPositions = new bool[numberOfArrays][];
        for (int i = 0; i < numberOfArrays; i++)
        {
            numbers[i] = Console.ReadLine()
                                .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => int.Parse(x))
                                .ToArray();
            visitedPositions[i] = new bool[numbers[i].Length];
        }
        Solve();

        Console.WriteLine(Math.Abs(maxSumNegative) + maxPath);
    }

    private static void Solve() 
    {
        for (int i = 0; i < 1; i++)
        {           
            for (int j = 0; j < numbers[i].Length; j++)
            {
                int currPath = 0;
                int sumOfNegative = 0;

                if (numbers[i][j] < 0 && !visitedPositions[i][j])
                {
                    sumOfNegative += numbers[i][j];
                    currPath++;
                    visitedPositions[i][j] = true;
                    CheckMax(currPath, sumOfNegative);               
                }
                else if (numbers[i][j] >= 0 && !visitedPositions[i][j])
                {
                    currPath++;
                    int tempCol = j;
                    int tempRow = i;
                    if (IsLastRow(i))
                    {
                        tempCol = numbers[i][j];
                        tempRow = 0;
                    }
                    else
                    {
                        tempCol = numbers[i][j];
                        tempRow++;
                    }
                   
                    while (numbers[tempRow][tempCol] >= 0 && !visitedPositions[tempRow][tempCol])
                    {
                        currPath++;
                        visitedPositions[tempRow][tempCol] = true;
                        if (IsLastRow(tempRow))
                        {
                            tempCol = numbers[tempRow][tempCol];
                            tempRow = 0;
                        }
                        else
                        {
                            tempCol = numbers[tempRow][tempCol];
                            tempRow++;
                        }
                    }
                    if (!visitedPositions[tempRow][tempCol])
                    {
                        sumOfNegative += numbers[tempRow][tempCol];
                        currPath++;
                        CheckMax(currPath, sumOfNegative);
                    }
                   
                }
            }
        }
    }

    private static bool IsLastRow(int row) 
    {
        if (row == numberOfArrays - 1)
        {
            return true;
        }
        return false;
    }

    private static void CheckMax(int path, int negativeSum) 
    {
        if ((Math.Abs(negativeSum) + path) > (Math.Abs(maxSumNegative) + maxPath))
        {
            maxSumNegative = negativeSum;
            maxPath = path;
        }
    }
}

