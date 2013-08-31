using BlogSystem.Data;
using BlogSystem.Models;
using BlogSystem.Services.Models;
using BlogSystem.Services.Persisters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogSystem.Services.Atributes;
using System.Web.Http.ValueProviders;

namespace BlogSystem.Services.Controllers
{
    public class PostsController : BaseApiController
    {
        public IQueryable<PostModel> GetAll(
           [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
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

                      
                        var posts = context.Posts.Select(post => new PostModel
                        {
                            Id = post.PostId,
                            Title = post.Title,
                            DateOfCreation = post.DateCreated,
                            PostedBy = post.UserOfPost.DisplayName,
                            Content = post.Text,
                            Comments = post.Comments.Select(comment => new FullCommentInfoModel { 
                                 Content = comment.Content,
                                 DateOfCreating=comment.DateCreated,
                                 UserOfComment=comment.UserOfComment.DisplayName
                            }),
                            Tags = post.Tags.Select(tag => tag.TagName)
                        }).OrderBy(x=>x.DateOfCreation).ToList();
                            
                        
                         return posts.AsQueryable();
                          
                    }
                });
                       return responseMsg;
    
        }


        public HttpResponseMessage GetPostsPaging(
           [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, int count, int page)
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

                        var posts = GetAll(sessionKey).Skip(page*count).Take(count).ToList();


                        var response =
                             this.Request.CreateResponse(HttpStatusCode.OK, posts);
                        return response;
                    }
                   
                });
            return responseMsg;
        }

        public HttpResponseMessage PostCreatePost(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey ,
            [FromBody]CreatePostModel postToAdd)
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
                        List<Tag> tags = new List<Tag>();
                       
                        var wordsFromTitle = postToAdd.Title.Split(new char[] { ' ', ',' },
                            StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < wordsFromTitle.Length; i++)
                        {
                            tags.Add(new Tag() { TagName = wordsFromTitle[i].ToLower() });
                        }
                      
                        for (int i = 0; i < postToAdd.Tags.Length; i++)
                        {
                            tags.Add(new Tag() { TagName = postToAdd.Tags[i].ToLower() });
                        }

                        var post = new Post()
                        {
                            Title = postToAdd.Title,
                            DateCreated = DateTime.Now,
                            UserOfPost = user,
                            Text = postToAdd.Text,
                            Tags = tags
                        };
                       
                        context.Posts.Add(post);
                        context.SaveChanges();

                        CreatedPostModel postModel = new CreatedPostModel() { Id = post.PostId, Title = post.Title };
                      

                        var response =
                            this.Request.CreateResponse(HttpStatusCode.Created,
                                postModel);
                        return response;
                    }
                });

            return responseMsg;
        }



        public HttpResponseMessage GetPostsByTags(
           [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, string tags)
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

                       
                        //fuck can't do it the easy way :( ...

                        var tagsSplited = tags.Split(',');


                        var postsResult = GetAll(sessionKey);
                      
                       
                        
                        var response =
                             this.Request.CreateResponse(HttpStatusCode.OK, postsResult);
                        return response;
                    }

                });
            return responseMsg;
        }



        public HttpResponseMessage GetPostKeyword([ValueProvider(typeof(HeaderValueProviderFactory<string>))]string sessionKey, string keyword)
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

                        var posts = GetAll(sessionKey).Where(x => x.Title.ToLower().Contains(keyword.ToLower())).ToList();
                        


                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK,
                                posts);
                        return response;
                    }
                });

            return responseMsg;
        }
}

}

