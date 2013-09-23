using EmailClientWPF.Commands;
using EmailClientWPF.Data;
using EmailClientWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmailClientWPF.ViewModels
{
    public class InboxViewModel : ViewModelBase, IPageViewModel
    {

        //public string Sender { get; set; }
        //public string Subject { get; set; }
        //public DateTime Date { get; set; }

        private ObservableCollection<FolderResponseMessage> messagesList;

        public IEnumerable<FolderResponseMessage> MessagesList
        {
            get
            {
                if (this.messagesList == null)
                {
                    this.MessagesList = DataPersister.GetInbox();
                }

                return this.messagesList;
            }
            set
            {
                if (this.messagesList == null)
                {
                    this.messagesList = new ObservableCollection<FolderResponseMessage>();
                }

                this.messagesList.Clear();
                foreach (var item in value)
                {
                    this.messagesList.Add(item);
                }
            }
        }


        public string Name
        {
            get
            {
                return "Inbox";
            }
        }

    }
}
