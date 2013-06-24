using System;
using System.Collections.Generic;

public class ShipDamage
{
    static void Main() 
    {
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine();
            HTMLValidator htmlValidator = new HTMLValidator();
            if (htmlValidator.Validate(line))
            {
                Console.WriteLine("VALID");
            }
            else
            {
                Console.WriteLine("INVALID");
            }
        }
    }
}

public class HTMLValidator 
{
    private Stack<string> tags;

    public HTMLValidator() 
    {
        this.tags = new Stack<string>();
    }

    public bool Validate(string htmlCode) 
    {
        string[] elements = htmlCode.Split(new char[] { '<' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < elements.Length; i++)
        {
            string element = elements[i];
            element = element.Substring(0, element.Length - 1); // removing <

            if (element[0] == '/') // we have clossing tag
            {
                element = element.Substring(1); // removing /

                if (tags.Count == 0)
                {
                    return false;
                }
                else 
                {
                    string elementFromStack = tags.Pop();
                    if (elementFromStack != element)
                    {
                        return false;
                    }
                }
            }
            else
            {
                tags.Push(element);
            }
        }

        if (tags.Count == 0)
        {
            return true;
        }
        return false;
    }
}