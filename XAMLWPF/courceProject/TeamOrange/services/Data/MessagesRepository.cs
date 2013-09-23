using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Data.Models;
using MongoDB.Driver.Builders;

namespace Data
{
    public class MessagesRepository
    {
        private MongoCollection receivedMessages;
        private MongoCollection sentMessages;
        private MongoCollection trash;
        public MongoDatabase Db { get; set; }

        public MessagesRepository(string connectionString, string database)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            this.Db = server.GetDatabase(database);
            this.receivedMessages = this.Db.GetCollection<DbMessageModel>("receivedMessages");
            this.sentMessages = this.Db.GetCollection<DbMessageModel>("sentMessages");
            this.trash = this.Db.GetCollection<DbMessageModel>("trash");
        }

        public void Add(DbMessageModel message)
        {
            this.receivedMessages.Insert(message);
        }

        public IQueryable<DbMessageModel> GetInbox(DbUserModel user)
        {
            return this.receivedMessages.AsQueryable<DbMessageModel>().Where(m => m.Username.ToLower() == user.Username.ToLower());
        }

        public IQueryable<DbMessageModel> GetSentItems(DbUserModel user)
        {
            return this.sentMessages.AsQueryable<DbMessageModel>().Where(m => m.Username.ToLower() == user.Username.ToLower());
        }

        public IQueryable<DbMessageModel> GetTrash(DbUserModel user)
        {
            return this.trash.AsQueryable<DbMessageModel>().Where(m => m.Username.ToLower() == user.Username.ToLower());
        }

        public DbMessageModel GetMessage(string messageId)
        {
            DbMessageModel message = null;

            message = this.receivedMessages.AsQueryable<DbMessageModel>().FirstOrDefault(m => m.Id == messageId);
            if (message == null)
            {
                message = this.sentMessages.AsQueryable<DbMessageModel>().FirstOrDefault(m => m.Id == messageId);
            }

            if (message == null)
            {
                message = this.trash.AsQueryable<DbMessageModel>().FirstOrDefault(m => m.Id == messageId);
            }

            return message;
        }

        public void DeleteMessage(string messageId)
        {
            DbMessageModel message = null;

            message = this.receivedMessages.AsQueryable<DbMessageModel>().FirstOrDefault(m => m.Id == messageId);
            if (message == null)
            {
                message = this.sentMessages.AsQueryable<DbMessageModel>().FirstOrDefault(m => m.Id == messageId);
            }
            else
            {
                this.receivedMessages.Remove(Query.EQ("_id", message.Id));
                this.trash.Insert(message);
                return;
            }

            if (message == null)
            {
                message = this.trash.AsQueryable<DbMessageModel>().FirstOrDefault(m => m.Id == messageId);
                this.trash.Remove(Query.EQ("_id", message.Id));
            }
            else
            {
                this.sentMessages.Remove(Query.EQ("_id", message.Id));
                this.trash.Insert(message);
                return;
            }
        }

        public void SaveMessage(DbMessageModel dbMessage)
        {
            this.sentMessages.Insert(dbMessage);
        }
    }
}
