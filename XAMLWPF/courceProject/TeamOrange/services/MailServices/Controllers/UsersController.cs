using Data;
using Data.Models;
using MailServices.AuthenticationHeaders;
using MailServices.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace MailServices.Controllers
{
    public class UsersController : BaseApiController
    {
        private const int TokenLength = 50;
        private const string TokenChars = "qwertyuiopasdfghjklmnbvcxzQWERTYUIOPLKJHGFDSAZXCVBNM";
        private const int MinUsernameLength = 6;
        private const int MaxUsernameLength = 30;
        private const int AuthenticationCodeLength = 40;
        private const string ValidUsernameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890_.@";

        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser(UserModel model)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                this.ValidateUser(model);

                var data = new UserRepository(
                ConfigurationManager.AppSettings["MongoConnectionString"],
                ConfigurationManager.AppSettings["Database"]);

                var dbUser = data.All().FirstOrDefault(u => u.Username.ToLower() == model.Username.ToLower());
                if (dbUser != null)
                {
                    throw new InvalidOperationException("This user already exists in the database");
                }

                dbUser = new DbUserModel()
                {
                    Username = model.Username,
                    AuthCode = model.AuthCode
                };

                data.Add(dbUser);

                var responseModel = new RegisterUserResponseModel()
                {
                    Id = dbUser.Id,
                    Username = dbUser.Username,
                };

                var response = this.Request.CreateResponse(HttpStatusCode.Created, responseModel);
                return response;
            });
        }

        [HttpPost]
        [ActionName("token")]
        public HttpResponseMessage LoginUser(UserModel model)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                this.ValidateUser(model);

                var data = new UserRepository(
                ConfigurationManager.AppSettings["MongoConnectionString"],
                ConfigurationManager.AppSettings["Database"]);

                var dbUser = new DbUserModel()
                {
                    Username = model.Username,
                    AuthCode = model.AuthCode
                };

                var user = data.GetLoggedUser(dbUser);

                if (user == null)
                {
                    throw new FormatException("Invalid username or password");
                }

                string token = null;

                if (user.AccessToken == null)
                {
                    token = data.SetAccessToken(user, this.GenerateAccessToken(user.Id));
                }
                else
                {
                    token = user.AccessToken;
                }

                var responseModel = new LoginResponseModel()
                {
                    Id = user.Id,
                    Username = user.Username,
                    AccessToken = token
                };

                var response = this.Request.CreateResponse(HttpStatusCode.OK, responseModel);
                return response;
            });
        }

        [HttpPut]
        [ActionName("logout")]
        public HttpResponseMessage LogoutUser(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
            string accessToken)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                var data = new UserRepository(
                ConfigurationManager.AppSettings["MongoConnectionString"],
                ConfigurationManager.AppSettings["Database"]);

                var user = this.GetUserByAccessToken(accessToken, data.Db);
                data.SetAccessToken(user, null);

                var response = this.Request.CreateResponse(HttpStatusCode.NoContent);
                return response;
            });
        }

        private string GenerateAccessToken(string userId)
        {
            StringBuilder tokenBuilder = new StringBuilder(TokenLength);
            tokenBuilder.Append(userId);
            while (tokenBuilder.Length < TokenLength)
            {
                var index = rand.Next(TokenChars.Length);
                var ch = TokenChars[index];
                tokenBuilder.Append(ch);
            }
            return tokenBuilder.ToString();
        }

        private void ValidateUser(UserModel userModel)
        {
            if (userModel == null)
            {
                throw new FormatException("Username and/or password are invalid");
            }
            this.ValidateUsername(userModel.Username);
            this.ValidateAuthCode(userModel.AuthCode);
        }

        private void ValidateAuthCode(string authCode)
        {
            if (string.IsNullOrEmpty(authCode) || authCode.Length != AuthenticationCodeLength)
            {
                throw new FormatException("Password is invalid");
            }
        }

        private void ValidateUsername(string username)
        {
            if (username.Length < MinUsernameLength || MaxUsernameLength < username.Length)
            {
                throw new FormatException(
                    string.Format("Username must be between {0} and {1} characters",
                        MinUsernameLength,
                        MaxUsernameLength));
            }
            if (username.Any(ch => !ValidUsernameChars.Contains(ch)))
            {
                throw new FormatException("Username contains invalid characters");
            }
        }
    }
}
