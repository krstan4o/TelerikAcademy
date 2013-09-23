using EmailClientWPF.Models;
using System;
using System.Linq;
using EmailClientWPF.Helpers;
using System.Windows.Forms;
using System.Net;
using System.Collections.Generic;
using System.Net.Mail;
using EmailClientWPF.Models;

namespace EmailClientWPF.Data
{
    public static class DataPersister
    {
        private static string AccessToken { get; set; }

        private const string BaseServicesUrl = "http://orangemail.apphb.com/api/"; // Change it...

        internal static void RegisterUser(string username, string authenticationCode)
        {
            UserHelper.ValidateUsername(username);                      
            UserHelper.ValidateAuthCode(authenticationCode);
          
            var userModel = new UserModel()
            {
                Username = username,
                AuthCode = authenticationCode
            };

            HttpRequester.Post(BaseServicesUrl + "users/register",
                userModel);
        }

        internal static string LoginUser(string username, string authenticationCode)
        {
            UserHelper.ValidateUsername(username);
            UserHelper.ValidateAuthCode(authenticationCode);
           
            var userModel = new UserModel()
            {
                Username = username,
                AuthCode = authenticationCode
            };
                   
            var loginResponse = HttpRequester.Post<LoginResponseModel>(BaseServicesUrl + "users/token",
                userModel);
            AccessToken = loginResponse.AccessToken;

            return loginResponse.Username.ToString();
        }

        internal static bool LogoutUser()
        {
            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;
            var isLogoutSuccessful = HttpRequester.Put(BaseServicesUrl + "users/logout", headers);
            return isLogoutSuccessful;
        }

        internal static IEnumerable<FolderResponseMessage> GetInbox()
        {
            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;
            var result = HttpRequester.Get<IEnumerable<FolderResponseMessage>>(BaseServicesUrl + "messages?folder=Inbox", headers);

            return result;
        }

        internal static void GetSendedMessages()
        {
            throw new NotImplementedException();
        }

        internal static void GetTrash()
        {
            throw new NotImplementedException();
        }

        internal static void SendMessage(string recepient, string subject, string message)
        {
            string[] recepients = recepient.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in recepients)
            {
                ValidateEmail(item);
            }

            FullMessageResponseModel model = new Models.FullMessageResponseModel() 
            {
                Id = "",
                Recipient = recepient,
                Sender = "",
                Subject = subject,
                BodyHtml = "",
                BodyPlain = message,
                Date = DateTime.Now
            };

            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;
            HttpRequester.Post<FullMessageResponseModel>(BaseServicesUrl + "messages/send", model, headers);
        }

        private static void ValidateEmail(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Email is invalid", ex);
            }
        }
    }
}
