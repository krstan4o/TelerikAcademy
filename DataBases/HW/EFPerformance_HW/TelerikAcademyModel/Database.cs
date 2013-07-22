using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikAcademyDB
{
    public class Database : DbContext
    {
        public Database()
            : base("TelerikAcademyEntities")
        {
            
        }

        public DbSet<Address> Addreses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects{ get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Town> Towns { get; set; }
    }
}
