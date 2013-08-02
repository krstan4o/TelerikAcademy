using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public DateTime? Date { get; set; }
        public Author Author { get; set; }
        public string TextReview { get; set; }
        public Review() 
        {
            
        }
    }
}
