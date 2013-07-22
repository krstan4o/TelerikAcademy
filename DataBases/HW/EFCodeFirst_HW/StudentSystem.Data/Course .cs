using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Data
{
    public class Course
    {
        [Key]
        public int CoursesId { get; set; }
        [MaxLength(30)]
        [MinLength(2)]
        [Required]
        public string Name { get; set; }
        public string Materials { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public Course() 
        {
            this.Students = new HashSet<Student>();
        }
    }
}
