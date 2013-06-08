using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasarelForum.Models
{
    public class PasarelDb : DbContext
    {
        public PasarelDb()
            : base("DefaultConnection")
        {
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<PasarelDb>()); 
        }
        
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumPostAnswer> ForumPostAnswers { get; set; }
    }
}
