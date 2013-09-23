using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Providers.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Data.Models;

namespace MailServices.Controllers
{
    public class BaseApiController : ApiController
    {
        protected static Random rand = new Random();

        protected T ExecuteOperationAndHandleExceptions<T>(Func<T> operation)
        {
            try
            {
                return operation();
            }
            catch (InvalidOperationException ex)
            {
                var errResponse = this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
                throw new HttpResponseException(errResponse);
            }
            catch (Exception ex)
            {
                var errResponse = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                throw new HttpResponseException(errResponse);
            }
        }

        protected DbUserModel GetUserByAccessToken(string accessToken, MongoDatabase database)
        {
            var users = database.GetCollection<DbUserModel>("users");
            var user = users.AsQueryable<DbUserModel>().FirstOrDefault(u => u.AccessToken == accessToken);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid user credentials");
            }

            return user;
        }
    }
}
