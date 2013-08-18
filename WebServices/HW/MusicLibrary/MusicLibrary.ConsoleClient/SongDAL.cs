using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicLibrary.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MusicLibrary.ConsoleClient
{
    public static class SongDAL
    {
        internal static void AddSongJson(Song Song)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119")
            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));


            Task<HttpResponseMessage> response = client.PostAsJsonAsync<Song>("api/Songs", Song);
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Song added. With json.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.Result.StatusCode, response.Result.ReasonPhrase);

        }

        internal static void AddSongXML(Song newSong)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.PostAsXmlAsync<Song>("api/Songs", newSong).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song added. With XML.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

        }

        internal static void UpdateSongXML(int SongId, Song newSong)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.PutAsXmlAsync<Song>("api/Songs/" + SongId, newSong).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song updated. With XML.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

        }

        internal static void UpdateSongJson(int SongId, Song newSong)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.PutAsJsonAsync<Song>("api/Songs/" + SongId, newSong).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song updated. With Json.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

        }

        internal static void GetAllSongsJson()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/Songs").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
        }

        internal static void GetAllSongsXML()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.GetAsync("/api/Songs").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
        }

        internal static void DeleteSongById(int SongId)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.DeleteAsync("/api/Songs/" + SongId).Result;
            if (response.IsSuccessStatusCode)
            {

                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Song Deleted:");
                Console.WriteLine(responseBody);

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
