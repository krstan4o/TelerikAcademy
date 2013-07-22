using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketEntity.Model;



namespace Supermarket.Data
{
    public class MarketSystemContext : DbContext
    {
        public DbSet<Product> Products{ get; set; }
        public DbSet<Measure> Courses { get; set; }
        public DbSet<Vendor> Vendors { get; set; } 
        public DbSet<Sale> Sales{ get; set; } 
    }
}
