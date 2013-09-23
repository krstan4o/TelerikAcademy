using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EmailClientWPF.Commands;
using EmailClientWPF.Data;
using EmailClientWPF.Helpers;

namespace EmailClientWPF.ViewModels
{
    public class NewMessageViewModel : ViewModelBase, IPageViewModel
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        private ICommand sendCommand;
        public event EventHandler<SendMessageArgs> MessageSent;

        public string Name
        {
            get
            {
                return "New Message";
            }
        }

        public ICommand Send
        {
            get
            {
                if (this.sendCommand == null)
                {
                    this.sendCommand =
                        new RelayCommand(this.HandleChangeViewModelCommand);
                }

                return this.sendCommand;
            }
        }
  
        private void HandleChangeViewModelCommand(object obj)
        {
            try
            {
                DataPersister.SendMessage(this.Recipient, this.Subject, this.Message);
                this.RaiseMessageSent();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void RaiseMessageSent()
        {
            var messageSuccessEvent = this.MessageSent;
            if (messageSuccessEvent != null)
            {
                messageSuccessEvent(this, null);
            }
        }
    }
}
