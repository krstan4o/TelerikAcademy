using System;
using Newtonsoft.Json;

namespace Winamp.Models
{
    public class Command
    {
        [JsonProperty("param")]
        public int Param { get; set; }
    }
}
