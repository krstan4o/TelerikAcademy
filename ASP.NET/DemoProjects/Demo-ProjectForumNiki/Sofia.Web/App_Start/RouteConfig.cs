using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sofia.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // /Questions/{postId}/{*catchAll}
            routes.MapRoute(
                name: "Forum post",
                url: "Questions/{postId}/{*catchAll}",
                defaults: new
                {
                    controller = "ForumPosts",
                    action = "ViewPost",
                }
                );

            // /Users/Nikolay
            routes.MapRoute(
                name: "Users",
                url: "Profile/{username}/{action}",
                defaults: new
                {
                    controller = "Users",
                    action = "ByUsername",
                    username = UrlParameter.Optional,
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}