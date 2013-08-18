using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public SongGenre SongGenre { get; set; }
        public Artist Artist { get; set; }

        public Song()
        {
          
        }
    }
}
