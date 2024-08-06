using MongoDB.Driver;
using Newtonsoft.Json;
using OriolOr.Maneko.API.Domain.IdentityManagement;
using OriolOr.Maneko.API.Infrastructure.Interfaces;
using OriolOr.Maneko.API.Properties;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;

namespace OriolOr.Maneko.API.Infrastructure
{
    public class UserDataRepository : IUserDataRepository
    {
        public string CollectionName => "UserData";
        private IMongoCollection<UserCredentials> UserDataCollection;

        public UserDataRepository()
        {
            this.UserDataCollection = MongoDbConfigurator.DataBase.GetCollection<UserCredentials>(this.CollectionName);
        }


        public void AddUser(UserCredentials userCredentials)
            => this.UserDataCollection.InsertOne(userCredentials);
        
        public IEnumerable<string> GetAllTokens() 
            => this.UserDataCollection.Find(FilterDefinition<UserCredentials>.Empty).ToList().Select(u => u.Token.Value).ToList();

        public string GetPasswordEncoded(string userName)
            => this.UserDataCollection.Find(usr => usr.UserName == userName).FirstOrDefault<UserCredentials>().Password;

        public string GetUserToken(string userName) 
            => this.UserDataCollection.Find(usr => usr.UserName == userName).FirstOrDefault().Token.Value.ToString();

        public void LoadUserData(IMongoDatabase database)
        {
            var nDocuments = this.UserDataCollection.CountDocuments(FilterDefinition<UserCredentials>.Empty);

            if (nDocuments == 0)
            {
                UserCredentials userData = JsonConvert.DeserializeObject<Collection<UserCredentials>>(Resources.UserData).FirstOrDefault();
                this.UserDataCollection.InsertOne(userData);
            }
        }
        public void SetUserToken(string userName, string token, uint expirationDate)
        {
            var userDoc = this.UserDataCollection.Find(usr => usr.UserName == userName).FirstOrDefault();

            userDoc.Token = new UserToken()
            {
                Value = token,
                ExpirationTime = expirationDate
            };

            this.UserDataCollection.ReplaceOne(usr => usr.UserName == userName, userDoc);
        }

        public bool UserExists(string userName)
            => this.UserDataCollection.CountDocuments(d => d.UserName == userName) == 1;

    }
}
