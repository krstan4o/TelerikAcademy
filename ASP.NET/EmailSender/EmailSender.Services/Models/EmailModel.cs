using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailSender.Services.Models
{
    public class EmailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}