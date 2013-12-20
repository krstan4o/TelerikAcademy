using System;
using Newtonsoft.Json;


namespace Winamp.Models
{
    public class PlayListSong
    {
        [JsonProperty("id")]
        public int Position { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("length")]
        public string Length { get; set; }

    }
}
