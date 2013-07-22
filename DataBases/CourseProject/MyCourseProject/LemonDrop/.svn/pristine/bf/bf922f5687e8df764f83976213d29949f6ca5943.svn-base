using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketEntity.Model
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Vendor"), Column("VendorID")]
        public int VendorId { get; set; }
        [Required,Column("Product Name")]
        public string ProductName { get; set; }
        [Required,ForeignKey("Measure"), Column("MeasureID")]
        public int MeasureId { get; set; }
        [Required, Column("Best Price")]
        public decimal BestPrice { get; set; }
    }
}
