using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.ExtendStringBuilder
{
    class TestSubstringExt
    {
        static void Main() 
        {
            StringBuilder strBuild = new StringBuilder("What the fuck are you talkink about");
            Console.WriteLine(strBuild.SubString(9, 4));
        }
    }
}
