using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinkBreaker
{
    public struct MoveData
    {
        public bool Success { get; set; }
        public sbyte[] Overlaps { get; set; }
        public string Message { get; set; }
        public bool GameEnded { get; set; }
    }
}