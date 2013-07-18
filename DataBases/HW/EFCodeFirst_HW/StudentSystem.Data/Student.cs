using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Data
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public Student() 
        {
            this.Courses = new HashSet<Course>();
        }
    }
}
