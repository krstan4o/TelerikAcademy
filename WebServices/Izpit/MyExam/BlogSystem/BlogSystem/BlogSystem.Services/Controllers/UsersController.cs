using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogSystem.Services.Models;
using BlogSystem.Models;
using BlogSystem.Data;
using BlogSystem.Services.Persisters;
using BlogSystem.Services.Atributes;
using System.Web.Http.ValueProviders;

namespace BlogSystem.Services.Controllers
{
    public class UsersController : BaseApiController
    {
        #region constants
        
      
        #endregion
        
        /*
        {  "username": "DonchoMinkov",
        "displayName": "Doncho Minkov",
        "authCode":   "bfff2dd4f1b310eb0dbf593bd83f94dd8d34077e" }
        
        */
        
        public UsersController()
        {
        }
        
        [ActionName("register")]
        public HttpResponseMessage PostRegisterUser(UserModel model)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    var context = new BlogDb();
                    using (context)
                    {
                        UserPersister.ValidateUsername(model.Username);
                        UserPersister.ValidateNickname(model.DisplayName);
                        UserPersister.ValidateAuthCode(model.AuthCode);
                        
                        var usernameToLower = model.Username.ToLower();
                        var nicknameToLower = model.DisplayName.ToLower();
                        
                        var user = context.Users.FirstOrDefault(
                            usr => usr.Username == usernameToLower ||
                                   usr.DisplayName.ToLower() == nicknameToLower);
                        
                        if (user != null)
                        {
                            throw new InvalidOperationException("User exists");
                        }
                        
                        user = new User()
                        {
                            Username = usernameToLower,
                            DisplayName = model.DisplayName,
                            AuthCode = model.AuthCode
                        };
                        
                        context.Users.Add(user);
                        context.SaveChanges();

                        user.SessionKey = UserPersister.GenerateSessionKey(user.UserId);
                        context.SaveChanges();
                        
                        var loggedModel = new UserLoggedModel()
                        {
                            DisplayName = user.DisplayName,
                            SessionKey = user.SessionKey
                        };
                        
                        var response =
                            this.Request.CreateResponse(HttpStatusCode.Created,
                                loggedModel);
                        return response;
                    }
                });
            
            return responseMsg;
        }
        
        [ActionName("login")]
        public HttpResponseMessage PostLoginUser(UserModel model)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    var context = new BlogDb();
                    using (context)
                    {
                        UserPersister.ValidateUsername(model.Username);
                        UserPersister.ValidateAuthCode(model.AuthCode);
                        
                        var usernameToLower = model.Username.ToLower();
                        
                        var user = context.Users.FirstOrDefault(
                            usr => usr.Username == usernameToLower &&
                                   usr.AuthCode == model.AuthCode);
                        
                        if (user == null)
                        {
                            throw new InvalidOperationException("Invalid username or password");
                        }
                        if (user.SessionKey == null)
                        {
                            user.SessionKey = UserPersister.GenerateSessionKey(user.UserId);
                            context.SaveChanges();
                        }
                        
                        var loggedModel = new UserLoggedModel()
                        {
                            DisplayName = user.DisplayName,
                            SessionKey = user.SessionKey
                        };
                        
                        var response =
                            this.Request.CreateResponse(HttpStatusCode.Created,
                                loggedModel);
                        return response;
                    }
                });
            
            return responseMsg;
        }
        
        [HttpPut]
        [ActionName("logout")]
        public HttpResponseMessage PutLogoutUser([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    var context = new BlogDb();
                    using (context)
                    {
                        UserPersister.ValidateSessionKey(sessionKey);

                        var user = context.Users.FirstOrDefault(usr => usr.SessionKey == sessionKey);
                        if (user == null)
                        {
                            throw new InvalidOperationException("Invalid session key.");
                        }
                        else
                        {
                            user.SessionKey = null;
                            context.SaveChanges();
                        }
                    }
                    
                    var response =
                        this.Request.CreateResponse(HttpStatusCode.OK);
                    
                    return response;
                });
            
            return responseMsg;
        }
          
      
    }
}
