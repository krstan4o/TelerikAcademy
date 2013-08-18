using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text.RegularExpressions;

namespace GetEqualStringCountLib
{
    public class Service : IService
    {
        public int GetEqualStringsCount(string text, string searched)
        {
            int foundCount = 0, lastIndex = 0;

            lastIndex = text.IndexOf(searched);
            while (lastIndex != -1)
            {
                foundCount++;
                lastIndex = text.IndexOf(searched, lastIndex + 1);
            }

            return foundCount;
        }
    }
}
