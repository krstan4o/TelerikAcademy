using System;
using System.Linq;
using System.Threading.Tasks;
using MusicLibrary.Models;
using System.Net.Http;
using System.Net.Http.Headers;
namespace MusicLibrary.ConsoleClient
{
    public static class ArtistDAL
    {
        internal static void AddArtistJson(Artist artist)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119")
            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
             

            Task<HttpResponseMessage> response = client.PostAsJsonAsync<Artist>("api/artists", artist);
            if (response.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist added. With json.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.Result.StatusCode, response.Result.ReasonPhrase);

        }

        internal static void AddArtistXML(Artist newArtist)
        {
            var client = new HttpClient
              {
                  BaseAddress = new Uri("http://localhost:55119"),
                   
              };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));
          
                HttpResponseMessage response = client.PostAsXmlAsync<Artist>("api/artists", newArtist).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist added. With XML.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

        }

        internal static void UpdateArtistXML(int artistId, Artist newArtist)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.PutAsXmlAsync<Artist>("api/artists/" + artistId, newArtist).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist updated. With XML.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

        }

        internal static void UpdateArtistJson(int artistId, Artist newArtist)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.PutAsJsonAsync<Artist>("api/artists/" + artistId, newArtist).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist updated. With Json.");

            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);

        }

        internal static void GetAllArtistsJson()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/artists").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
        }

        internal static void GetAllArtistsXML()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.GetAsync("/api/artists").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
        }

        internal static void DeleteArtistById(int artistId)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55119"),

            };
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.DeleteAsync("/api/artists/" + artistId).Result;
            if (response.IsSuccessStatusCode)
            {

                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Artist Deleted:");
                Console.WriteLine(responseBody);
               
            }
            else
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
        }
    }
}