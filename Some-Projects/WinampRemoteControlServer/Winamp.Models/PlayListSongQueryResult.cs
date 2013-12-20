using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
namespace Winamp.Models
{
    public class PlayListSongQueryResult
    {
        [JsonProperty("total")]
        public int Total { get; set; }
         [JsonProperty("songs")]
        public IQueryable<PlayListSong> Songs { get; set; }
    }
}
