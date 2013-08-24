using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogSystem.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        public string TagName { get; set; }

        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
