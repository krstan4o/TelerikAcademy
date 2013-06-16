using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.Models
{
    public class ForumPost
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name="Content")]
        public string Content { get; set; }
        public UserProfile Author { get; set; }
        public DateTime CreationTime { get; set; }
        public IEnumerable<ForumPostAnswer> Answers { get; set; }
    }
}
