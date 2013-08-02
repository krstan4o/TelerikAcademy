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
    public class ProductRaport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [JsonProperty ("product-id")]
        public int ProductId { get; set; }
        [JsonProperty("product-name")]
        public string ProductName { get; set; }
        [JsonProperty("Vendor-name")]
        public string Vendor_name { get; set; }
        [JsonProperty("total-quantity-sold")]
        public double TotalQuantitySold { get; set; }
        [JsonProperty("total-incomes")]
        public double TotalIncomes { get; set; }
    }
}
