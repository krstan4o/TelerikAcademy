using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    /// <summary>
    /// My ext method for finding the longest
    /// sequence of equal elements.
    /// </summary>
    /// <param name="sequenceOfNumbers"></param>
    /// <returns><int> List longestSeqOfEqualNumbers</int></returns>
    public static List<int> RetriveEqualSubsequence(this List<int> sequenceOfNumbers) 
    {
        var maxSeqOfEqualElements = sequenceOfNumbers.Select((n, i) => new { Value = n, Index = i })
                                   .OrderBy(s => s.Value)
                                   .Select((o, i) => new { Value = o.Value, Diff = i - o.Index })
                                   .GroupBy(s => new { s.Value, s.Diff })
                                   .OrderByDescending(g => g.Count())
                                   .First()
                                   .Select(f => f.Value)
                                   .ToList<int>();
        return maxSeqOfEqualElements; //todo
    }
    /// <summary>
    /// My method for removing all negative numbers.
    /// </summary>
    /// <param name="sequenceOfNumbers"></param>
    public static void RemoveNegativeNumbers(this List<int> sequenceOfNumbers)
    {
        sequenceOfNumbers.RemoveAll(x => x < 0); 
    }
}
