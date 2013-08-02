using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Bookstore.Data;

namespace Bookstore.Model
{
    public class BookstoreDb : DbContext
    {
        public BookstoreDb()
            : base("BookstoreEntities")
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
