using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
class Program
{

  
 static Dictionary<string, string> cipherDict = new Dictionary<string, string>();
 static List<string> result = new List<string>();
      
    static void Main()
    {
        string codedMessage = Console.ReadLine().Trim();
        
        foreach (Match item in Regex.Matches(Console.ReadLine(),@"(\D)(\d+)"))
            cipherDict[item.Groups[1].Value] = item.Groups[2].Value;
      
    



        Decode(codedMessage, "");

        //Printing
        result.Sort();
        Console.WriteLine(result.Count);
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }

    }

    static void Decode(string encoded, string decoded)
    {
        if (encoded.Length == 0)
            result.Add(decoded);

        else foreach (var cipher in cipherDict)
                if (encoded.StartsWith(cipher.Value))
                    Decode(encoded.Substring(cipher.Value.Length), decoded + cipher.Key);
    }


  
}

