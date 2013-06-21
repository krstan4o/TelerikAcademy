using System;

class Program
{
    static void Main()
    {
        bool[] arr = new bool[10000001];
        
        for (int i = 2; i <= 10000000; i++)
        {
            arr[i] = true;
        }
        for (int i = 2; i <=Math.Sqrt(10000000); i++)
        {
            if (arr[i])
            {
                for (int j = (i); j <= 10000000; j+=i)
                {
                    if (j==i)
                    {
                        continue;
                    }
                    arr[j] = false;
                }
            }
        }
            //Printing
        for (int i = 2; i <= 10000000 ; i++)
        {
            if (arr[i])
            {
                Console.Write(i+", ");
            }
        }
    }
}

