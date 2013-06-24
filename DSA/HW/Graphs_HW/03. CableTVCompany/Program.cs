using GraphLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.CableTVCompany
{
    public class CableTVCompany
    {
        public static void Main()
        {
            Graph<int> graph = new Graph<int>();
            graph.AddConnection(1, 2, 14, true);
            graph.AddConnection(1, 3, 10, true);
            graph.AddConnection(2, 4, 14, true);
            graph.AddConnection(1, 5, 18, true);
            graph.AddConnection(2, 6, 9, true);
            graph.AddConnection(3, 6, 10, true);
            graph.AddConnection(3, 4, 14, true);
            graph.AddConnection(7, 8, 9, false);
            graph.AddConnection(4, 7, 15, false);
            graph.AddConnection(4, 8, 11, true);
            graph.AddConnection(9, 10, 7, false);
            graph.AddConnection(1, 10, 10, true);
            graph.AddConnection(3, 9, 11, true);
            graph.AddConnection(8, 11, 8, false);
            graph.AddConnection(11, 12, 12, true);
            graph.AddConnection(9, 12, 17, true);
            graph.AddConnection(5, 10, 13, true);
            graph.AddConnection(6, 12, 15, true);
            graph.AddConnection(7, 13, 7, true);
            graph.AddConnection(8, 13, 10, true);
            graph.AddConnection(11, 13, 13, true);
            graph.AddConnection(7, 12, 12, true);
            graph.AddConnection(11, 14, 7, true);
            graph.AddConnection(12, 14, 10, false);

            List<Edge<int>> edges = graph.PrimeMinimumSpanningTree(1);
            Console.WriteLine("The edges of the minimum spanning tree from node 1 is:");
            foreach (var edje in edges)
            {
                Console.WriteLine(edje);
            }
        }
    }
}