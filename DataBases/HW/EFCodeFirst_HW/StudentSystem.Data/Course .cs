using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Data
{
    public class Course
    {
        [Key]
        public int CoursesId { get; set; }
        public string Name { get; set; }
        public string Materials { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public Course() 
        {
            this.Students = new HashSet<Student>();
        }
    }
}
