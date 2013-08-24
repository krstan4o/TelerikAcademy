using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlogSystem.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual User UserOfComment { get; set; }

        public Comment()
        {
        }
    }
}
