using MusicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace MusicLibrary.Data
{
    public class MusicLibraryDB : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
       
        public MusicLibraryDB() : base("MusicLibrary") 
        {
          
        }
    }
}
