using EmailClientWPF.Commands;
using EmailClientWPF.Data;
using EmailClientWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace EmailClientWPF.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private ICommand changeViewModelCommand;
        private IPageViewModel currentViewModel;
        private bool loggedInUser = false;
        private ICommand logoutCommand;

        public string Username { get; set; }

        public IPageViewModel CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
            set
            {
                this.currentViewModel = value;
                this.OnPropertyChanged("CurrentViewModel");
            }
        }

        public bool LoggedInUser
        {
            get
            {
                return this.loggedInUser;
            }
            set
            {
                this.loggedInUser = value;
                this.OnPropertyChanged("LoggedInUser");
            }
        }

        //Add new view models as properties here and then add to the ViewModels.Add in constructor
        public LoginRegisterFormViewModel LoginRegisterVM { get; set; }
        public List<IPageViewModel> ViewModels { get; set; }
        
        //Log out
        public ICommand Logout
        {
            get
            {
                if (this.logoutCommand == null)
                {
                    this.logoutCommand = new RelayCommand(this.HandleLogoutCommand);
                }
                return this.logoutCommand;
            }
        }

        //When we logout user change the current view to LogginRegisterView
        private void HandleLogoutCommand(object obj)
        {
            bool isUserLoggedOut = DataPersister.LogoutUser();
            if (isUserLoggedOut)
            {
                this.Username = "";
                this.LoggedInUser = false;
                //this.CurrentViewModel = this.LoginRegisterVM;
                this.HandleChangeViewModelCommand(this.LoginRegisterVM);
            }
        }

        //Change the Current ViewModel
        public ICommand ChangeViewModel
        {
            get
            {
                if (this.changeViewModelCommand == null)
                {
                    this.changeViewModelCommand =
                        new RelayCommand(this.HandleChangeViewModelCommand);
                }
                return this.changeViewModelCommand;
            }
        }

        private void HandleChangeViewModelCommand(object parameter)
        {
            var newCurrentViewModel = parameter as IPageViewModel;
            this.CurrentViewModel = newCurrentViewModel;
        }

        //Add instance the view models and the first view = LoginRegisterView
        public AppViewModel()
        {
            this.ViewModels = new List<IPageViewModel>();
            var loginVM = new LoginRegisterFormViewModel();
            var newMessageVM = new NewMessageViewModel();

            this.ViewModels.Add(newMessageVM);
            this.ViewModels.Add(new InboxViewModel());
            this.ViewModels.Add(new SentViewModel());
            this.ViewModels.Add(new TrashViewModel());

            loginVM.LoginSuccess += this.LoginSuccessful;
            newMessageVM.MessageSent += this.MessageSend;
            this.LoginRegisterVM = loginVM;
            this.CurrentViewModel = this.LoginRegisterVM;
        }

        //When user loggs in succesful change the view to see his EmailBox with all the views - Inbox, SendedMessages, Trash and LogOutButton
        
        public void LoginSuccessful(object sender, LoginSuccessArgs e)
        {
            this.Username = e.Username;
            this.LoggedInUser = true;
            this.HandleChangeViewModelCommand(this.ViewModels[1]);
        }

        public void MessageSend(object sender, SendMessageArgs e)
        {
            this.HandleChangeViewModelCommand(this.ViewModels[1]);
        }
    }
}
