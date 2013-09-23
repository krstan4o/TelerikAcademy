using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MailServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "UsersApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new {controller =  "users"});

            config.Routes.MapHttpRoute(
                name: "MessageReceive",
                routeTemplate: "api/messages",
                defaults: new { controller = "messages" }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
