using System;
using System.Linq;
using System.Text;


namespace _01.ExtendStringBuilder
{
    public static class ExtendStringBuilder
    {
        public static StringBuilder SubString(this StringBuilder input, int index, int length)
        {
            return new StringBuilder(input.ToString().Substring(index,length));
        }
    }
}