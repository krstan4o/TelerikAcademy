using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BlogSystem.Services.Models
{
    [DataContract]
    public class CreatedPostModel
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name="id")]
        public int Id { get; set; }
    }
}