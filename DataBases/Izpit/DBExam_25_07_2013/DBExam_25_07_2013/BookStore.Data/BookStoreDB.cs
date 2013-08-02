using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    //Task 1 - 2 are in Code First aproach and I em using DataAnotattions Atributes for adding constraints
    public class BookStoreDB : DbContext
    {
        public BookStoreDB()
            : base("BookStoreDb") //The conection string in App.conf
        { 
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
