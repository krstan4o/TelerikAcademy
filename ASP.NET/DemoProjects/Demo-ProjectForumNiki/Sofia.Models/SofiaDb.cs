using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.Models
{
    public class SofiaDb : DbContext
    {
        public SofiaDb()
            : base("DefaultConnection")
        {
            
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ForumPostVote> ForumPostVotes { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumPostAnswer> ForumPostAnswers { get; set; }
    }
}
