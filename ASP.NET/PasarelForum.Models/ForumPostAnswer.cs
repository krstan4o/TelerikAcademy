using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasarelForum.Models
{
    public class ForumPostAnswer
    {
        [Key]
        public int ForumPostAnswerId { get; set; }
        public ForumPost ForumPost { get; set; }
        public string Content { get; set; }
        public DateTime AnswerTime { get; set; }
        public string Author { get; set; }
        public DateTime LastEdited { get; set; }
    }
}
