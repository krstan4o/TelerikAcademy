using System;
using System.Linq;

class Program
{
    //static bool[] isVisited;
    static int maxJumpsCount = 1;
    static void Main()
    {
        string[] inputSplited = Console.ReadLine().Split(
            new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        short[] numbers = ParseInputToNumber(inputSplited);

        //isVisited = new bool[numbers.Length];


        for (ushort step = 1; step < numbers.Length; step++)
        {
            for (ushort startPosition = 0; startPosition < numbers.Length; startPosition++)
            {
                int count = FindMaximalJumps(numbers, step, startPosition);
                if (count > maxJumpsCount)
                {
                    maxJumpsCount = count;
                }
            }
        }

        Console.WriteLine(maxJumpsCount); // print the result
    }

    private static int FindMaximalJumps(short[] numbers, ushort step, ushort startPosition)
    {
            ushort jumpsCount = 0;
        
            ushort currentPosition = startPosition;
            int lastPosition =int.MinValue;
            do
            {
                lastPosition = currentPosition;
                currentPosition += step;
                if (currentPosition >= numbers.Length)
                {
                    currentPosition -= (ushort)numbers.Length;
                }

                jumpsCount++;
              
            }
            while (numbers[currentPosition] < numbers[lastPosition]);
            return jumpsCount;
    }

    private static short[] ParseInputToNumber(string[] inputSplited)
    {
        short[] numbers = new short[inputSplited.Length];
        for (int i = 0; i < inputSplited.Length; i++)
        {
            numbers[i] = short.Parse(inputSplited[i]);
        }
        return numbers;
    }
}
