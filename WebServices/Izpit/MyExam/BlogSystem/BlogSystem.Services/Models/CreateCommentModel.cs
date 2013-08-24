using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BlogSystem.Services.Models
{
    [DataContract]
    public class CreateCommentModel
    {
        [DataMember(Name="text")]
        public string Text { get; set; }
    }
}