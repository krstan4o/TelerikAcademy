using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.Labirint3D
{
    class Program
    {
        static char[, ,] lab;
        static List<int> moves = new List<int>();
        static void Main(string[] args)
        {

            string[] input = Console.ReadLine().Split();
            int startLevel = int.Parse(input[0]);
            int startRow = int.Parse(input[1]);
            int startCol = int.Parse(input[2]);


            input = Console.ReadLine().Split();

            int L = int.Parse(input[0]);
            int R = int.Parse(input[1]);
            int C = int.Parse(input[2]);

            lab = new char[L, R, C];

            for (int reedLevel = 0; reedLevel < L; reedLevel++)
            {
                
                for (int reedRow = 0; reedRow < R; reedRow++)
                {
                    string currentRowLine = Console.ReadLine();
                    for (int reedCol = 0; reedCol < C; reedCol++)
                    {
                        lab[reedLevel,reedRow,reedCol]= currentRowLine[reedCol];
                    }
                }
            }

            FindPathToExit(startRow, startCol, startLevel, 0);
            int debug = 0;
            Console.WriteLine(moves.Min());
        }

        static bool InRange(int row, int col)
        {
           // bool levelInRange = level >= 0 && level < lab.GetLength(0);
            bool rowInRange = row >= 0 && row < lab.GetLength(1);
            bool colInRange = col >= 0 && col < lab.GetLength(2);
            
            return rowInRange && colInRange;
        }

        static void FindPathToExit(int row, int col, int level, int movesCounter)
        {

            if (!InRange(row, col))
            {
                // We are out of the labyrinth -> can't find a path
              
                return;
            }
           
            else if (level < 0 || level >= lab.GetLength(0))
            {

                moves.Add(movesCounter);
                return;
                // we escaped
            }
            else
            {
                if (lab[level, row, col] == '.')
                {
                    // Temporary mark the current cell as visited
                    lab[level, row, col] = 's';
                    
                    // Recursively explore all possible directions
                    FindPathToExit(row + 1, col, level, movesCounter + 1); // down
                    FindPathToExit(row, col + 1, level, movesCounter + 1); // right
                    FindPathToExit(row, col - 1, level, movesCounter +1); // left
                    FindPathToExit(row - 1, col, level, movesCounter+1); // up
                   
                    

                    // Mark back the current cell as free
                    //lab[level, row, col] = '.';
                   // movesCounter--;
                }
                else if (lab[level, row, col] == 'U')
                {
                    lab[level, row, col] = '.';
                    FindPathToExit(row, col, level + 1, movesCounter + 1);
                    //if (level + 1 >= lab.GetLength(0))
                    //{
                    //    return;
                    //}
                    
                }
                else if (lab[level, row, col] == 'D')
                {
                    lab[level, row, col] = '.';
                    FindPathToExit(row, col, level - 1, movesCounter + 1);
                    
                }
            }
        }
    }
}
