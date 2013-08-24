using BlogSystem.Models;
using System;
using System.Data.Entity;

namespace BlogSystem.Data
{
    public class BlogDb : DbContext
    {
        public BlogDb() : base("BlogDbConStr")
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }
    }
}