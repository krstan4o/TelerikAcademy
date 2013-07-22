using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketEntity.Model
{
    [Table("Sales")]
    public class Sale
    {
        ICollection<Product> products;

        [Key]
        public int Id { get; set; }
        [Required,ForeignKey("Product"), Column("Product_Id")]  
        public int ProductId { get; set; }
        [Required, Column("Qantity")]
        public string Quantity { get; set; }
        [Required, Column("Unit Price")]
        public decimal UnitPrice { get; set; }
        [Required, Column("Location")]
        public string Location { get; set; }
        [Required, Column("Sum")]
        public string Sum { get; set; }

        public virtual Product Product { get; set; }

    }
}