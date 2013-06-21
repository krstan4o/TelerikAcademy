using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter the arrays size...");
        int size = int.Parse(Console.ReadLine());
        int size1 = int.Parse(Console.ReadLine());
        int[] array = new int[size];
        int[] array1 = new int[size1];

        bool equal = true;
        if (size != size1)
        {
            Console.WriteLine("The two arrays are not equal.");

        }
        else
        {
            Console.WriteLine("Please enter {0} numbers for the first array.", size);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Please enter {0} numbers for the secound array.", size);
            for (int i = 0; i < array1.Length; i++)
            {
                array1[i] = int.Parse(Console.ReadLine());
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i]!=array1[i])
                {
                    equal = false;
                }
            }
            if (equal)
            {
                Console.WriteLine("The two arrays are equal.");
            }
            else
            {
                Console.WriteLine("The two arrays are not equal.");
            }
        }
    }
}

