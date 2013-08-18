using System;
using System.Linq;
using System.Web.Http;
using EmailSender.Services.Models;
using Typesafe.Mailgun;
using System.Net.Mail;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Security.Policy;
using System.Threading;

namespace EmailSender.Services.Controllers
{
    public class SendController : ApiController
    {
        WebBrowser wb;
       
        private static string Wtf = "";
        
        // POST api/send
          [STAThread]
        public HttpResponseMessage Post(EmailModel value)
        {
              var waiter = new ManualResetEvent(true);
            var staThread = new Thread(() =>
             {
                 var browser = new WebBrowser();
                 browser.DocumentCompleted += (sender, e) => 
                 {
           
                     waiter.Set(); // Signal the web service thread we're done.
                     Wtf = "fuckkkkkkkkkk";
                 };
                    browser.Navigate("http://www.google.com");
             });
    staThread.SetApartmentState(ApartmentState.STA);
    staThread.Start();

    var timeout = TimeSpan.FromSeconds(30);
    waiter.WaitOne(timeout); // Wait for the STA thread to finish.
   
                var client = new MailgunClient("app15162.mailgun.org", "key-8-br6rzagyq2r4593n-iqmx-lrlkf902");
            
            var message = new MailMessage(value.From, value.To, value.Title, value.Content);
            client.SendMail(message);
            
            return Request.CreateResponse(HttpStatusCode.OK, value);
}

          
        

      
        private void SearchForFooCallbackMethod()
        {
            Wtf = "asdasdas";

            wb = new WebBrowser();
            wb.Document.OpenNew(true); // Reset to new document, if don't do this the webBrowser1.Document.Write() just append more html without clear the previously code, and I learn (by the bad way) this duplicates attached events.
            wb.Navigate("about:blank");
            while (wb.Document == null && wb.Document.Body == null) 
            {
                
            }
            //wb.Document.Title = "fuckkkkkkkkk";
            //wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            //wb.Url = new Uri("http://google.com", System.UriKind.Absolute);
            //wb.Navigate("http://google.com");
            int debug=2;
           
        }
        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            wb.Document.GetElementById("n_1_2").SetAttribute("value", "0892024111");
            wb.Document.GetElementById("n_2").SetAttribute("value", "D@tas0l");
            Wtf = "eventtttt";
            HtmlElementCollection es = wb.Document.GetElementsByTagName("input");
            HtmlElement ele = es[2];
            ele.Focus();
            ele.InvokeMember("Click");
        }
    }
}
