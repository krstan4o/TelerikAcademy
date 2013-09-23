using EmailClientWPF.Commands;
using EmailClientWPF.Data;
using EmailClientWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace EmailClientWPF.ViewModels
{
    public class LoginRegisterFormViewModel:ViewModelBase,IPageViewModel
    {
        private ICommand registerCommand;
        private ICommand loginCommand;

        public string Username { get; set; }
        public string errorMessage { get; set; }

        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set
            {
                if (value != this.errorMessage)
                {
                    this.errorMessage = value;
                    base.OnPropertyChanged("ErrorMessage");
                }
            }
        }

        public ICommand Register
        {
            get
            {
                
                if (this.registerCommand == null)
                {
                    this.registerCommand = new RelayCommand(this.HandleRegisterCommand);
                }
                return this.registerCommand;
            }
        }

        public ICommand Login
        {
            get
            {
                if (this.loginCommand == null)
                {
                    this.loginCommand = new RelayCommand(this.HandleLoginCommand);
                }
                return this.loginCommand;
            }
        }

        private void HandleRegisterCommand(object parameter)
        {
            try
            {
                this.ErrorMessage = "";
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;
                UserHelper.ValidatePassword(password);
                var authenticationCode = this.GetSHA1HashData(password);
                DataPersister.RegisterUser(this.Username, authenticationCode);
                this.HandleLoginCommand(parameter);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

        }
        public event EventHandler<LoginSuccessArgs> LoginSuccess;

        private void HandleLoginCommand(object parameter)
        {
            this.ErrorMessage = "";
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            var authenticationCode = this.GetSHA1HashData(password);
            try
            {
                UserHelper.ValidatePassword(password);
                var username = DataPersister.LoginUser(this.Username, authenticationCode);

                if (!string.IsNullOrEmpty(username))
                {
                    this.RaiseLoginSuccess(username);
                }
            }
            catch (Exception ex) 
            {
                this.ErrorMessage = ex.Message;
            }

           
        }
        public LoginRegisterFormViewModel()
        {
            this.Username = "Username";
            this.ErrorMessage = "";
        }

        protected void RaiseLoginSuccess(string username)
        {
            if (this.LoginSuccess != null)
            {
                this.LoginSuccess(this, new LoginSuccessArgs(username));
            }
        }

        private string GetSHA1HashData(string data)
        {
            HashAlgorithm algorithm = SHA1.Create(); 
            var hash =  algorithm.ComputeHash(Encoding.UTF8.GetBytes(data));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        public string Name
        {
            get { return "Login Form"; }
        }

       
    }
}
