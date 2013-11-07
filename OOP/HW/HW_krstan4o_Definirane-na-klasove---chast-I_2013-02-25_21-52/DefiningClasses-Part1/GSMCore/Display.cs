using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMCore
{
    public class Display    //Task 1 - Define class Display in GSM class
    {
        private double size;
        private int colors;
        public Display()
        {
        }
        public Display(double size, int colors)
        {
            this.Size = size;
            this.Colors = colors;
        }

        public double Size 
        {
            get { return this.size; }
            set 
            {
                if (value < 0.1 || value > 10) 
                {
                    throw new ArgumentException("Please enter real display size");
                }
                this.size = value;
            }
        }

        public int Colors 
        {
            get { return this.colors; }
            set
            {
                if (value < 100 || value > int.MaxValue) 
                {
                    throw new ArgumentOutOfRangeException("Please enter number of colors of the GSM Display");
                }
                this.colors = value;
            }
        }
    }
}
