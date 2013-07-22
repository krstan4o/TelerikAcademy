using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketEntity.Model
{
    [Table("Supermarket")]
    public class Supermarket
    {
        [Key]
        public int Id { get; set; }
        [Required, Column("SupermarketName")]
        public string SupermarketName { get; set; }
       
    }
}
