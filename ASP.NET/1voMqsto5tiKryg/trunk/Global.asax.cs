using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using CGWeb;
using CGWeb.Games;
using CGWeb.Services;

namespace SinkBreaker
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapHubs();
            ServiceManager.Init();
            ServiceManager.RegisterGame<SinkBreakerGame, DefaultGameCreationArgs, SinkBreakerHub>((args) => new SinkBreakerGame());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Set a auth cookie and redirect if the login is successful
            if (Request.Path.Contains("login"))
            {
                var username = Request.Form["username"];
                if (!string.IsNullOrEmpty(username))
                {
                    var user = new GuestUser(username);
                    user.TryLogOn();
                    FormsAuthentication.SetAuthCookie(username, false);
                    Response.Redirect("multy-game.html");
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}