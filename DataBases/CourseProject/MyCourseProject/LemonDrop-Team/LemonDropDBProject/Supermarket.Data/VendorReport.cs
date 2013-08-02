using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data
{
    public class VendorReport
    {
        public string VendorName { get; set; }
        public double Incomes { get; set; }
        public double Expenses { get; set; }
        public double Taxes { get; set; }
        private double Result { get; set; }
    }
}
