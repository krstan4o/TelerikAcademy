using System;
using System.Collections.Generic;
using System.Linq;

namespace GetEqualStringCount.ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GetEqualStringsCountServiceReference.ServiceClient client = new GetEqualStringsCountServiceReference.ServiceClient();
            int count = client.GetEqualStringsCount("asdaadasdaasasasddddas", "as");
            Console.WriteLine(count);
        }
    }
}
