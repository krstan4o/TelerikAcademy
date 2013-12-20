using System;
using Newtonsoft.Json;

namespace Winamp.Models
{
    public class CurrentSongInfo
    {
        [JsonProperty("winampStatus")]
        public string WinampStatus { get; set; }
        [JsonProperty("id")]
        public int Positon { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("pathToFile")]
        public string PathToFile { get; set; }
        [JsonProperty("artist")]
        public string Artist { get; set; }
        [JsonProperty("album")]
        public string Album { get; set; }
        [JsonProperty("year")]
        public string Year { get; set; }
        [JsonProperty("bitrate")]
        public string Bitrate { get; set; }
        [JsonProperty("length")]
        public string Length { get; set; }

        [JsonProperty("elapsed")]
        public string Elapsed { get; set; }
    }
}
