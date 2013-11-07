using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.EnumarableExtMethods
{
    public static class EnumarableExt
    {
        public static T Sum<T>(this IEnumerable<T> input)
        {
            dynamic result = 0;
            foreach (var item in input)
            {
                result += item;
            }
            return result;
        }
        public static T Product<T>(this IEnumerable<T> input)
        {
            dynamic result = 1;
            foreach (var item in input)
            {
                result *= item;
            }
            return result;
        }
        public static T Average<T>(this IEnumerable<T> input)
        {
            dynamic result = 0;
            foreach (var item in input)
            {
                result += item;
            }
            return result / input.Count();
        }
        public static T Min<T>(this IEnumerable<T> input)
        {
            dynamic min = input.ElementAt(0);
            for (int i = 1; i < input.Count(); i++)
            {
                if (min > input.ElementAt(i)) min = input.ElementAt(i);
            }
            return min;
        }
        public static T Max<T>(this IEnumerable<T> input)
        {
            dynamic max = input.ElementAt(0);
            for (int i = 1; i < input.Count(); i++)
            {
                if (max < input.ElementAt(i)) max = input.ElementAt(i);
            }
            return max;
        }
    }
}
