using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicLibrary.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MusicLibrary.ConsoleClient
{
    public static class AlbumDAL
    {
        internal static void AddAlbumJson(Album Album)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119")
            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));


            Task<HttpResponseMessage> response = client.PostAsJsonAsync<Album>("api/Albums", Album);
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Album added. With json.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.Result.StatusCode, response.Result.ReasonPhrase);

        }

        internal static void AddAlbumXML(Album newAlbum)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.PostAsXmlAsync<Album>("api/Albums", newAlbum).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album added. With XML.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

        }

        internal static void UpdateAlbumXML(int AlbumId, Album newAlbum)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.PutAsXmlAsync<Album>("api/Albums/" + AlbumId, newAlbum).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album updated. With XML.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

        }

        internal static void UpdateAlbumJson(int AlbumId, Album newAlbum)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.PutAsJsonAsync<Album>("api/Albums/" + AlbumId, newAlbum).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album updated. With Json.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

        }

        internal static void GetAllAlbumsJson()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/Albums").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
        }

        internal static void GetAllAlbumsXML()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.GetAsync("/api/Albums").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
        }

        internal static void DeleteAlbumById(int AlbumId)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.DeleteAsync("/api/Albums/" + AlbumId).Result;
            if (response.IsSuccessStatusCode)
            {

                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Album Deleted:");
                Console.WriteLine(responseBody);

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
