using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClientWPF.Models
{
    // This is the model when we send message to server and when we want to view only one message in the inbox, sended, trash.)
    public class FullMessageResponseModel
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string BodyPlain { get; set; }
        public string BodyHtml { get; set; }
        public DateTime Date { get; set; }
    }
}
