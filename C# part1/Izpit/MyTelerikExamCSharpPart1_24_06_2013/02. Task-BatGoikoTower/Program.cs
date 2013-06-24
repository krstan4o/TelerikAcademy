using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        char whatToWrite = '\0';
        int count = 2;

        int countForPoints = 0;
        int countForPointsExided = 0;
        for (int numberOfRow = 0; numberOfRow < n; numberOfRow++)
        {
            for (int i = numberOfRow; i <= n-2; i++)
            {
                Console.Write(".");
            }
            Console.Write("/");
            if (numberOfRow != 0)
            {
                if (countForPoints != countForPointsExided)
                {
                    whatToWrite = '.';
                    countForPointsExided++;
                }
                else 
                {
                    whatToWrite = '-';
                    countForPoints++;
                    countForPointsExided = 0;
                }
                for (int i = 0; i < count; i++)
                {
                    Console.Write(whatToWrite);
                }
                count += 2;
            }
            Console.Write("\\");
            for (int i = numberOfRow; i <= n - 2; i++)
            {
                Console.Write(".");
            }
            Console.WriteLine();
        }
    }
}

