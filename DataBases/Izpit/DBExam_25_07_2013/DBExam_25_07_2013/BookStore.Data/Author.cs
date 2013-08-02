using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookStore.Data
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
        public Author() 
        {
            this.Books = new HashSet<Book>();
        }
    }
}
