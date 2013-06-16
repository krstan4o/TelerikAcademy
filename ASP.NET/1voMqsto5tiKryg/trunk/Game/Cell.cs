using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinkBreaker
{
    public class Cell
    {
        public Cell TopNeighbour { get; set; }
        public Cell BottomNeighbour { get; set; }
        public Cell LeftNeighbour { get; set; }
        public Cell RightNeighbour { get; set; }

        public Cell TopLeftNeighbour { get; set; }
        public Cell TopRightNeighbour { get; set; }
        public Cell BottomLeftNeighbour { get; set; }
        public Cell BottomRightNeighbour { get; set; }
        public string Position { get; protected set; }
        public sbyte ResidentTilesCount { get; protected set; }

        public Cell(int x, int y)
        {
            ResidentTilesCount = 0;
            Position = string.Format("{0} {1}", x, y);
        }

        public bool Covered()
        {
            return this.ResidentTilesCount > 0;
        }

        public void AdjustResidents(sbyte amount)
        {
            if (ResidentTilesCount + amount < 0)
            {
                throw new ArgumentOutOfRangeException("Can't have less than 0 residents at a cell!");
            }
            ResidentTilesCount += amount;
        }
    }
}