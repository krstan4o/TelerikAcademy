using System;

namespace MailServices.Models
{
    public class MessageResponceModel
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