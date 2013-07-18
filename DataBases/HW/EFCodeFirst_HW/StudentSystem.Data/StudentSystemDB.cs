using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace StudentSystem.Data
{
    public class StudentSystemDB : DbContext
    {
        public StudentSystemDB(string name = "StudentSystem") 
            :base(name)
        {
            
        }
        public StudentSystemDB()  
            :this("StudentSystem")
        {
        
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
    }
}
