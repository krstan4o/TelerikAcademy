using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Data.Models
{
    public class DbUserModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string Username { get; set; }
        public string AuthCode { get; set; }
        public string AccessToken { get; set; }
    }
}
