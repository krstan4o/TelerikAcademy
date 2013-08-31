using BlogSystem.Data;
using BlogSystem.Services.Atributes;
using BlogSystem.Services.Models;
using BlogSystem.Services.Persisters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.ValueProviders;

namespace BlogSystem.Services.Controllers
{
    public class TagsController : BaseApiController
    {
        public HttpResponseMessage GetAll([ValueProvider(typeof(HeaderValueProviderFactory<string>))]string sessionKey)
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

                        var tags = new List<TagsModel>();

                        var tagsFromDB = context.Tags.Include("Posts").ToList();

                        for (int i = 0; i < tagsFromDB.Count(); i++)
                        {
                            tags.Add(new TagsModel()
                            {
                                Id = tagsFromDB[i].TagId,
                                Name = tagsFromDB[i].TagName,
                                Posts = tagsFromDB[i].Posts.Count
                            });
                        }

                        tags.OrderBy(x => x.Id);

                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK,
                                tags);
                        return response;
                    }
                });
            return responseMsg;
        }

        public HttpResponseMessage GetTagsById([ValueProvider(typeof(HeaderValueProviderFactory<string>))]string sessionKey, int tagId)
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

                       
                        var tag = context.Tags.FirstOrDefault(x=>x.TagId == tagId);

                        PostsController controler = new PostsController();


                        if (tag==null)
	                    {
		                    throw new InvalidOperationException("Invalid tag ");
	                    }

                        var posts = controler.GetAll(sessionKey).Where(x => x.Tags.Contains(tag.TagName)).ToList();
                        

                       

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
    
