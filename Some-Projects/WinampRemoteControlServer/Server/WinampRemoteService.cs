using Server.Controllers;
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;
using System.Linq;
using System.Threading;


namespace Server
{
    class WinampRemoteService
    {
        public const string ServiceAddress = "http://localhost:8000";      
     
        static void Main()
        {
            Thread th = new Thread(StartServer);
            th.Start();
            Console.WriteLine("The winamp remote control service was started.\nYou can minimize this window and control winamp with your Mobile Phone\n or press any key to stop the server and close the program.");
            Console.ReadKey();
        }

        static void StartServer()
        {

            var config = new HttpSelfHostConfiguration(ServiceAddress);

            config.Routes.MapHttpRoute(
                "API Default", "{controller}/{action}/{id}",
                new
                {
                    id = RouteParameter.Optional,
                    controller = new WinampController()
                });

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();          
        }
    }  
}
