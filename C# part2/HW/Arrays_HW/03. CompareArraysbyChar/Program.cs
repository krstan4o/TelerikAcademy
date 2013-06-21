using System;

class Program
{
    static void Main()
    {
          Console.WriteLine("Please enter the arrays size...");
        int size = int.Parse(Console.ReadLine());
        int size1 = int.Parse(Console.ReadLine());
        char[] array = new char[size];
        char[] array1 = new char[size1];

        bool equal = true;
        if (size == size1)
        {
            Console.WriteLine("Please enter {0} characters for the first array.", size);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = char.Parse(Console.ReadLine());
            }

            Console.WriteLine("Please enter {0} characters for the secound array.", size);
            for (int i = 0; i < array1.Length; i++)
            {
                array1[i] = char.Parse(Console.ReadLine());
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == array1[i])
                {
                    continue;
                }
                else
                {
                    if (array[i] < array1[i])
                    {
                        Console.WriteLine("The first array is lexicografically before the secound.");
                    }
                    else
                    {
                        Console.WriteLine("The first array is lexicografically after the secound.");
                    }
                    return;
                }
                
            }

            if (equal)
            {
                Console.WriteLine("The two char arrays are equal.");
            }
           

        }
        else if(size<size1)
        {
            Console.WriteLine("The first array is lexicografically before the secound one.");
        }
        else 
        {
            Console.WriteLine("The first array is lexicografically after the secound one.");
        }
    }
}

