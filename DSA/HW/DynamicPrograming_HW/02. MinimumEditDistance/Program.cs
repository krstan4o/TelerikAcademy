

namespace MinimumEditDistance
{
    using System;

    /// <summary>
    /// Write a program to calculate the "Minimum Edit Distance" (MED) between two words. MED(x, y) is the minimal sum of costs of edit operations used to transform x to y. Sample costs are given below:
    /// cost (replace a letter) = 1
    /// cost (delete a letter) = 0.9
    /// cost (insert a letter) = 0.8
    /// Example: x = "developer", y = "enveloped" -> cost = 2.7
    /// delete ‘d’:  "developer" -> "eveloper", cost = 0.9
    /// insert ‘n’:  "eveloper" -> "enveloper", cost = 0.8
    /// replace ‘r’ -> ‘d’:  "enveloper" -> "enveloped", cost = 1
    ///
    /// Link with explanation: http://www.let.rug.nl/kleiweg/lev/
    /// </summary>
    public class MinimumEditDistance
    {
        private const decimal CostDelete = 0.9M;
        private const decimal CostInsert = 0.8M;
        private const decimal CostReplace = 1M;
        private static decimal[,] table;

        public static void Main()
        {
            var result1 = Compute("developer", "enveloped");
            Console.WriteLine("Words: developer -> enveloped");
            Console.WriteLine("Distance = {0}", result1);
            PrintCostsTable();
            Console.WriteLine();

            var result2 = Compute("developer", "eveloper");
            Console.WriteLine("Words: developer -> eveloper");
            Console.WriteLine("Distance = {0}", result2);
            PrintCostsTable();
            Console.WriteLine();

            var result3 = Compute("eveloper", "enveloper");
            Console.WriteLine("Words: eveloper -> enveloper");
            Console.WriteLine("Distance = {0}", result3);
            PrintCostsTable();
            Console.WriteLine();

            var result4 = Compute("eveloper", "");
            Console.WriteLine("Words: eveloper ->  ");
            Console.WriteLine("Distance = {0}", result4);
            PrintCostsTable();
            Console.WriteLine();

            var result5 = Compute("", "eveloper");
            Console.WriteLine("Words:  -> enveloper");
            Console.WriteLine("Distance = {0}", result5);
            PrintCostsTable();
            Console.WriteLine();

            var result6 = Compute("", "");
            Console.WriteLine("Words:  -> ");
            Console.WriteLine("Distance = {0}", result6);
            PrintCostsTable();
        }

        /// <summary>
        /// Compute the distance between two words.
        /// </summary>
        public static decimal Compute(string word1, string word2)
        {
            int n = word1.Length;
            int m = word2.Length;
            table = new decimal[n + 1, m + 1];

            // Step 1: Fill cost of deletions
            for (int row = 0; row <= n; row++)
            {
                table[row, 0] = row * CostDelete;
            }

            // Step 2: Fill cost of insertions
            for (int col = 0; col <= m; col++)
            {
                table[0, col] = col * CostInsert;
            }

            // Step 3: Calculate and choose min cost operation
            // ----------------------------------
            // |diag Cell  | above Cell           |
            // -----------------------------------
            // |           | min (above + delete, |
            // |left Cell  |      diag + replace, |
            // |           |      left + insert)  |
            // ----------------------------------
            for (int row = 1; row <= n; row++)
            {
                // Step 4
                for (int col = 1; col <= m; col++)
                {
                    // Step 5: Calculate the cost of replacing. If the letters are equal it is 0, otherwise its the replace operation cost
                    decimal cost = (word2[col - 1] == word1[row - 1]) ? 0 : CostReplace;

                    // Step 6: Find the minimal cost operation of the 3 possible
                    decimal delete = table[row - 1, col] + CostDelete;
                    decimal replace = table[row - 1, col - 1] + cost;
                    decimal insert = table[row, col - 1] + CostInsert;

                    table[row, col] = Math.Min(
                        Math.Min(delete, insert),
                        replace);
                }
            }

            // Step 7: Take and return the result (most down-right cell)
            return table[n, m];
        }

        public static void PrintCostsTable()
        {
            Console.WriteLine("Costs table");
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write("{0, 4} ", table[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}

