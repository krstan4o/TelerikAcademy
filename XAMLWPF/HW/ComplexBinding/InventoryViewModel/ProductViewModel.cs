using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Category { get; set; }
        
        public string  ProductNumber { get; set; }

        public string ProductModel { get; set; }

        public decimal? UnitCost { get; set; }

        public string Description { get; set; }
    }
}
