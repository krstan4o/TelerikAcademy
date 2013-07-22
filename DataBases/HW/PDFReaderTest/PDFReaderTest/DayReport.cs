using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFReaderTest
{
    public class DayReport
    {
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string MarketName { get; set; }
        public double Sum { get; set; }

        public DayReport(string productName, string quantity, double unitPrice, string marketName, double sum)
        {
            this.ProductName = productName;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.MarketName = marketName;
            this.Sum = sum;
        }

    }
}
