using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Supermarket.Data
{
    public class VendorExpenses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }      
        [JsonProperty("Vendor-name")]
        public string Vendor_name { get; set; }
        [JsonProperty("data")]
        public string Date { get; set; }
        [JsonProperty("Expenses")]
        public double Expenses { get; set; }
    }
}
