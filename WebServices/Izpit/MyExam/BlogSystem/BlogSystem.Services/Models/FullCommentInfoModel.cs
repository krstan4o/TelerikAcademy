using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BlogSystem.Services.Models
{
    [DataContract]
    public class FullCommentInfoModel
    {
        [DataMember(Name = "text")]
        public string Content { get; set; }
        [DataMember(Name = "commentedBy")]
        public string UserOfComment { get; set; }
        [DataMember(Name = "postDate")]
        public DateTime DateOfCreating { get; set; }
    }
}