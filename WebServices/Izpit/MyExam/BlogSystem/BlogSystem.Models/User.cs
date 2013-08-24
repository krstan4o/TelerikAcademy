using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string AuthCode { get; set; }

        [StringLength(50, MinimumLength = 50)]
        public string SessionKey { get; set; }

        public User()
        {
            this.Posts = new HashSet<Post>();
           
            this.Comments = new HashSet<Comment>();
        }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
