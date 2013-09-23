using Data.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;

namespace Data
{
    public class UserRepository
    {
        private MongoCollection users;
        public MongoDatabase Db { get; set; }

        public UserRepository(string connectionString, string database)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            this.Db = server.GetDatabase(database);
            this.users = this.Db.GetCollection<DbUserModel>("users");
        }

        public void Add(DbUserModel user)
        {
            this.users.Insert(user);
        }

        public IQueryable<DbUserModel> All()
        {
            return this.users.AsQueryable<DbUserModel>();
        }

        public DbUserModel GetLoggedUser(DbUserModel user)
        {
            DbUserModel resultUser = this.users.AsQueryable<DbUserModel>()
                .FirstOrDefault(u => u.Username.ToLower() == user.Username.ToLower() 
                    && u.AuthCode == user.AuthCode);

            return resultUser;
        }

        public string SetAccessToken(DbUserModel user, string token)
        {
            var dbUser = this.users.FindOneByIdAs<DbUserModel>(user.Id);
            dbUser.AccessToken = token;
            this.users.Save(dbUser, SafeMode.True);
            return dbUser.AccessToken;
        }
    }
}
