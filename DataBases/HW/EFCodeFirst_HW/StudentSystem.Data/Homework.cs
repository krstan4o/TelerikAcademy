using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Data
{
    public class Homework
    {
        [Key]
        public int HomeworkId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime TimeSend { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

        public Homework() 
        {
           
        }

    }
}
