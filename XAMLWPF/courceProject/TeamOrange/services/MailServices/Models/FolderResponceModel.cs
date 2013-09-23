using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailServices.Models
{
    public class FolderResponceModel
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
    }
}