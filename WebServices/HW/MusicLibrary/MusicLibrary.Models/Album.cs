using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public string ProducerName { get; set; }
        public ICollection<Song> Songs { get; set; }
        public ICollection<Artist> Artists { get; set; }
        public Album()
        {
            this.Songs = new HashSet<Song>();
            this.Artists = new HashSet<Artist>();
        }
    }
}
