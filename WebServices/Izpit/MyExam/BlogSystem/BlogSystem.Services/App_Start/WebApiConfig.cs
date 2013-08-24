using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlogSystem.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

          
            //api/tags/{tagId}/posts
            config.Routes.MapHttpRoute(
            name: "TagsApi",
            routeTemplate: "api/tags/{tagId}/posts",
            defaults: new
            {
                controller = "tags",
                action = "gettagsbyid",

            }
        );

           // /posts/{postId}/comment
            config.Routes.MapHttpRoute(
             name: "CommentsApi",
             routeTemplate: "api/posts/{postId}/comment",
             defaults: new
             {
                 controller = "comments",
                 action = "createcomment",
                
             }
         );
            
            config.Routes.MapHttpRoute(
              name: "UsersApi",
              routeTemplate: "api/users/{action}",
              defaults: new
              {
                  controller = "users"
              }
          );
            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
