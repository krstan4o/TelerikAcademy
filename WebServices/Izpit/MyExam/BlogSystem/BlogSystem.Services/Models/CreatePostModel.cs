using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BlogSystem.Services.Models
{
    [DataContract]
    public class CreatePostModel
    {
        [DataMember(Name="title")]
        public string Title { get; set; }

        [DataMember(Name="text")]
        public string Text { get; set; }

        [DataMember(Name="tags")]
        public string[] Tags { get; set; }
    }
}