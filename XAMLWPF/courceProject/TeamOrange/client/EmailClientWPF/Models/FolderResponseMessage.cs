using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClientWPF.Models
{
    // This is the model when we want to view the messages in the inbox,sended,trash 
    // Server will return Collection<FolderResponseMessage>()
    public class FolderResponseMessage
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
    }
}
