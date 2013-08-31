using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogSystem.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; }
       
        public virtual User UserOfPost { get; set; }
       
        public Post()
        {
            this.Comments = new HashSet<Comment>();
            this.Tags = new HashSet<Tag>();
        }
       
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
