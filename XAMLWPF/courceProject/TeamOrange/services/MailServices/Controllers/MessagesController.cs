using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Data;
using Data.Models;
using System.Configuration;
using MailServices.Models;
using System.Web.Http.ValueProviders;
using MailServices.AuthenticationHeaders;
using RestSharp;

namespace MailServices.Controllers
{
    public class MessagesController : BaseApiController
    {
        private const string MailDomain = "app22696.mailgun.org"; 

        [HttpPost]
        [ActionName("receive")]
        public HttpResponseMessage ReceiveMessage()
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;

                var data = new MessagesRepository(
                    ConfigurationManager.AppSettings["MongoConnectionString"],
                    ConfigurationManager.AppSettings["Database"]);

                var recipient = httpRequest.Unvalidated.Form["recipient"].Split('@');
                var recipientName = recipient[0].Trim();

                DbMessageModel model = new DbMessageModel()
                {

                    BodyHtml = httpRequest.Unvalidated.Form["body-html"],
                    BodyPlain = httpRequest.Unvalidated.Form["body-plain"],
                    Date = DateTime.Now,
                    Sender = httpRequest.Unvalidated.Form["from"],
                    Recipient = httpRequest.Unvalidated.Form["recipient"],
                    Subject = httpRequest.Unvalidated.Form["subject"],
                    Username = recipientName
                };

                data.Add(model);

                var responseModel = new MessageResponceModel
                {
                    Id = model.Id,
                    BodyHtml = model.BodyHtml,
                    BodyPlain = model.BodyPlain,
                    Date = model.Date,
                    Recipient = model.Recipient,
                    Sender = model.Sender,
                    Subject = model.Subject
                };

                var response = this.Request.CreateResponse(HttpStatusCode.Created, responseModel);
                return response;
            });
        }

        [HttpPost]
        [ActionName("send")]
        public HttpResponseMessage SendMessage(MessageResponceModel message,
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
            string accessToken)
        {
                var data = new MessagesRepository(
                        ConfigurationManager.AppSettings["MongoConnectionString"],
                        ConfigurationManager.AppSettings["Database"]);
                var user = this.GetUserByAccessToken(accessToken, data.Db);

                if (user == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

                message.Sender = user.Username + "@" + MailDomain;
                ValidateMessage(message);
                SendMessageToMailgun(message);
                DbMessageModel dbMessage = new DbMessageModel()
                {
                    BodyHtml = message.BodyHtml,
                    BodyPlain = message.BodyPlain,
                    Date = message.Date,
                    Recipient = message.Recipient,
                    Sender = message.Sender,
                    Subject = message.Subject,
                    Username = user.Username
                };

                data.SaveMessage(dbMessage);
                var response = this.Request.CreateResponse(HttpStatusCode.Created, message);
                return response;
        }

        public HttpResponseMessage GetFolder(string folder,
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
            string accessToken) 
        {
            var data = new MessagesRepository(
                    ConfigurationManager.AppSettings["MongoConnectionString"],
                    ConfigurationManager.AppSettings["Database"]);
            var user = this.GetUserByAccessToken(accessToken, data.Db);

            if (user == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            IQueryable<DbMessageModel> dbFolder = null;

            switch (folder)
            {
                case "Inbox":
                    dbFolder = data.GetInbox(user);
                    break;
                case "Sent":
                    dbFolder = data.GetSentItems(user);
                    break;
                case "Trash":
                    dbFolder = data.GetTrash(user);
                    break;
                default:

                    break;
            }
            var responceFolder = from message in dbFolder
                                select new FolderResponceModel()
                                {
                                    Date = message.Date,
                                    Id = message.Id,
                                    Recipient = message.Recipient,
                                    Sender = message.Sender,
                                    Subject = message.Subject
                                };

            var response = this.Request.CreateResponse(HttpStatusCode.OK, responceFolder);
            return response;
        }

        public HttpResponseMessage GetMessage(string messageId,
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
            string accessToken)
        {
            var data = new MessagesRepository(
                    ConfigurationManager.AppSettings["MongoConnectionString"],
                    ConfigurationManager.AppSettings["Database"]);
            var user = this.GetUserByAccessToken(accessToken, data.Db);

            if (user == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var message = data.GetMessage(messageId);
            if (message == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, messageId);
            }

            var responceMessage = new MessageResponceModel()
                                 {
                                     BodyHtml = message.BodyHtml,
                                     BodyPlain = message.BodyPlain,
                                     Date = message.Date,
                                     Id = message.Id,
                                     Recipient = message.Recipient,
                                     Sender = message.Sender,
                                     Subject = message.Subject
                                 };

            var response = this.Request.CreateResponse(HttpStatusCode.OK, responceMessage);
            return response;
        }

        public HttpResponseMessage DeleteMessage(string messageId,
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
            string accessToken)
        {
            var data = new MessagesRepository(
                    ConfigurationManager.AppSettings["MongoConnectionString"],
                    ConfigurationManager.AppSettings["Database"]);

            var user = this.GetUserByAccessToken(accessToken, data.Db);

            if (user == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var message = data.GetMessage(messageId);
            if (message == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, messageId);
            }

            data.DeleteMessage(messageId);

            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            return response;

        }

        private void ValidateMessage(MessageResponceModel message)
        {
            if (message.BodyHtml != null && message.BodyPlain == null)
            {
                message.BodyPlain = "You need a HTML capable viewer to read this message!";
            }

            if (message.Recipient == null)
            {
                throw new ArgumentNullException("Recipient is required!");
            }

            string[] recepients = message.Recipient.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var recepient in recepients)
            {
                ValidateEmail(recepient);
            }
        }

        private void ValidateEmail(string email)
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

        private IRestResponse SendMessageToMailgun(MessageResponceModel message)
        {
            string apiKey = ConfigurationManager.AppSettings["MAILGUN_API_KEY"];
            string[] recepients = message.Recipient.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               apiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 MailDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", message.Sender);
            foreach (var recipient in recepients)
            {
                request.AddParameter("to", recipient);
            }
            
            request.AddParameter("subject", message.Subject);
            request.AddParameter("text", message.BodyPlain);
            request.AddParameter("html", message.BodyHtml);
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
