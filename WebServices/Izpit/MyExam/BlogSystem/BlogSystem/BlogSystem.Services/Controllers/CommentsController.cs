using BlogSystem.Data;
using BlogSystem.Models;
using BlogSystem.Services.Atributes;
using BlogSystem.Services.Models;
using BlogSystem.Services.Persisters;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace BlogSystem.Services.Controllers
{
    public class CommentsController : BaseApiController
    {
        [HttpPut]
        public HttpResponseMessage CreateComment([ValueProvider(typeof(HeaderValueProviderFactory<string>))]string sessionKey,
            int postId, [FromBody]CreateCommentModel comment)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    var context = new BlogDb();
                    using (context)
                    {

                        UserPersister.ValidateSessionKey(sessionKey);

                        var user = context.Users.FirstOrDefault(
                            usr => usr.SessionKey == sessionKey);

                        if (user == null)
                        {
                            throw new InvalidOperationException("Invalid username or password");
                        }

                        var post = context.Posts.FirstOrDefault(p => p.PostId == postId);
                        if (post == null)
                        {
                             throw new ArgumentNullException("Invalid post id");
                        }

                        Comment commToAdd = new Comment()  
                        { 
                             Content= comment.Text,
                             UserOfComment = user,
                             DateCreated = DateTime.Now
                        };

                        post.Comments.Add(commToAdd);
                        context.SaveChanges();

                        var response =
                           this.Request.CreateResponse(HttpStatusCode.OK);
                        return response;
                    }
                });
            return responseMsg;
        }
    }
}