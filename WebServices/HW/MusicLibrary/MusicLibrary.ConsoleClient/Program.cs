using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary.Models;
using System.Net.Http;
using System.Net.Http.Headers;
namespace MusicLibrary.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            Artist newArtist = new Artist
            {
                BirthDate = new DateTime(1988, 2, 5),
                Country = "Bulgaria",
                FirstName = "Milko",
                LastName = "Kalaidjiev"
            };

            // Add artist 2 times one with json secound with xml.
            ArtistDAL.AddArtistJson(newArtist);
            ArtistDAL.AddArtistXML(newArtist);

            //Update artist with json id = 1;
            ArtistDAL.UpdateArtistJson(1, new Artist()
            {
                ArtistId = 1,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Country = "Bulgaria",
                BirthDate = new DateTime(1987, 02, 03)

            });

            //Update artist with XML id=2;
            ArtistDAL.UpdateArtistXML(2, new Artist()
            {
                ArtistId = 2,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Country = "Bulgaria",
                BirthDate = new DateTime(1987, 02, 03)
            });

            // Get All Artists
            ArtistDAL.GetAllArtistsJson();
            ArtistDAL.GetAllArtistsXML();

            //Delete Artist by id
            ArtistDAL.DeleteArtistById(1);


            Song newSong = new Song()
            {
                SongGenre = SongGenre.Chalgiqqq,
                Title = "Ku4ekkkkk Ventilatora",
                Year = new DateTime(2009,2,2)
            };
            //Add songs 2 times one with json secound with xml.
            SongDAL.AddSongJson(newSong);
            SongDAL.AddSongXML(newSong);


            //Update song with json id = 1;
            SongDAL.UpdateSongJson(1, new Song()
            {
                SongId = 1,
                Title = "Pavle mi pie",
                SongGenre = SongGenre.Jazz,
                Year = new DateTime(1987, 02, 03)
            });

            //Update song with XML id=2;
            SongDAL.UpdateSongXML(2, new Song()
            {
                SongId = 2,
                Title = "Pavle Mi Pie",
                SongGenre = SongGenre.Jazz,
                Year = new DateTime(1987, 02, 03)
            });

            //Get All Songs
            SongDAL.GetAllSongsJson();
            SongDAL.GetAllSongsXML();

            //Delete Artist by id
            SongDAL.DeleteSongById(2);
           

            Album newAlbum = new Album
            {
              Title = "Ku4eci ot mahalata 5",
              Year = new DateTime(1998,02,03),
              ProducerName = "Bai ivan",
              Songs = new Song[] {
                  new Song{  Title = "Pavle Mi Pie", SongGenre = SongGenre.Chalgiqqq, Year = new DateTime(1987, 02, 03)},
                   new Song{  Title = "Grobarite", SongGenre = SongGenre.Chalgiqqq, Year = new DateTime(1987, 02, 03)},
              },
               Artists = new Artist[] {
                    new Artist{ BirthDate = new DateTime(1988, 2, 5), Country = "Bulgaria", FirstName = "Milko", LastName = "Kalaidjiev" },
                    new Artist{ BirthDate = new DateTime(1977, 2, 5), Country = "Bulgaria", FirstName = "Gosho", LastName = "Dupeto" },
               }
            };

            //Add albums 2 times one with json secound with xml.
            AlbumDAL.AddAlbumJson(newAlbum);
            AlbumDAL.AddAlbumXML(newAlbum);

            //Update Album using json.
            AlbumDAL.UpdateAlbumJson(1, new Album
            {
                AlbumId = 1,
                Title = "Ku4eci ot mahalata 2",
                Year = new DateTime(1993, 02, 03),
                ProducerName = "Bai Kaputo"
              
            });

            //Update Album using xml

            AlbumDAL.UpdateAlbumXML(2, new Album
            {
                AlbumId = 2,
                Title = "Ku4eci ot mahalata 2",
                Year = new DateTime(1993, 02, 03),
                ProducerName = "Bai Kaputo"
             
            });

            // GetAll albums with json and xml
            AlbumDAL.GetAllAlbumsXML();
            AlbumDAL.GetAllAlbumsJson();

            //Delete album by id
            AlbumDAL.DeleteAlbumById(1);
            

            // When deleting albums Exeption for Foreign key constraint...
        }


    }
}
