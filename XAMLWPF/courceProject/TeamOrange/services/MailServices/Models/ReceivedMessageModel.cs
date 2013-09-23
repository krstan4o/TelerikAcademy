using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MailServices.Models
{
    public class ReceivedMessageModel
    {
        [JsonProperty("recipient")]
        public string Recipient { get; set; }
        [JsonProperty("from")]
        public string Sender { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("body-plain")]
        public string BodyPlain { get; set; }
        [JsonProperty("body-html")]
        public string BodyHtml { get; set; }
        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }
    }
}