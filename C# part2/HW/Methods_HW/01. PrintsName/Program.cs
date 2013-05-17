using System;

public class Program
{
    static void Main()
    {
        Console.Write("Please enter your name: ");
     
        Console.WriteLine(PrintsName(Console.ReadLine()));
    }


    public static string PrintsName(string name)
    {
        bool isVallidName = true;
        for (int i = 0; i < name.Length; i++)
        {
            if (!char.IsLetter(name,i))
            {
                isVallidName = false;
            }
        }
        if (isVallidName)
        {
            return "Hello, " + name + ".";   
        }
        else
        {
            return "Invalid name.";   
        }
        
    }
}


