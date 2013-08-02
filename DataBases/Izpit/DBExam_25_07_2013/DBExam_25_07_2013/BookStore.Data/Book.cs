using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        [MaxLength(250)]
        public string Website { get; set; }
        // [Unique] if we insert null values it is not unique
        [MaxLength(13)]
        public string Isbn { get; set; }
        public decimal? Price { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Book() 
        {
            this.Authors = new HashSet<Author>();
            this.Reviews = new HashSet<Review>();
        }
    }
}
