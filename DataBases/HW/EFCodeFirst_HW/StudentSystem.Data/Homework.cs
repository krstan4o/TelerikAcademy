using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Data
{
    public class Homework
    {
        [Key]
        public int HomeworkId { get; set; }
        public DateTime Content { get; set; }
        public DateTime TimeSend { get; set; }
        public Student Student { get; set; }
        private ICollection<Course> courses { get; set; }

        public Homework() 
        {
            this.courses = new HashSet<Course>();
        }

        public virtual ICollection<Course> Courses 
        {
            get { return this.courses; }
            set { this.courses = value; }
        }
    }
}
