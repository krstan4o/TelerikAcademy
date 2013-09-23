using EmailClientWPF.Commands;
using EmailClientWPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmailClientWPF.ViewModels
{
    public class MainBoxViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get { return "Main Box View"; }
        }

        // When clicked on new Message button
        private ICommand createMessageCommand { get; set; }

        public ICommand CreateMessage
        {
            get
            {
                if (this.createMessageCommand == null)
                {
                    this.createMessageCommand = new RelayCommand(this.HandleCreateMessageCommand);
                }
                return this.createMessageCommand;
            }
        }

        private void HandleCreateMessageCommand(object obj)
        {
            throw new NotImplementedException(); // todo get the view to write a message and button send to server.
        }

        //Get user inbox
        private ICommand getInboxCommand { get; set; }

        public ICommand GetInbox
        {
            get
            {
                if (this.getInboxCommand == null)
                {
                    this.getInboxCommand = new RelayCommand(this.HandleGetInboxCommand);
                }
                return this.getInboxCommand;
            }
        }

        private void HandleGetInboxCommand(object obj)
        {
            DataPersister.GetInbox(); // todo
        }

        private ICommand getSendedCommand { get; set; }

        public ICommand GetSended
        {
            get
            {
                if (this.getSendedCommand == null)
                {
                    this.getSendedCommand = new RelayCommand(this.HandleGetSendedCommand);
                }
                return this.getSendedCommand;
            }
        }

        private void HandleGetSendedCommand(object obj)
        {
            DataPersister.GetSendedMessages(); //todo
        }

        private ICommand getTrashCommand { get; set; }

        public ICommand GetTrash
        {
            get
            {
                if (this.getTrashCommand == null)
                {
                    this.getTrashCommand = new RelayCommand(this.HandleGetTrashCommand);
                }
                return this.getTrashCommand;
            }
        }

        private void HandleGetTrashCommand(object obj)
        {
            DataPersister.GetTrash(); //todo
        }

       
    }
}
