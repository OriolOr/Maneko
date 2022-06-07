﻿using MongoDB.Driver;
using Newtonsoft.Json;
using OriolOr.Maneko.Domain.IdentityManagement;
using OriolOr.Maneko.Infrastructure.Properties;
using System.Collections.ObjectModel;

namespace OriolOr.Maneko.Infrastructure
{
    public  class UserDataRepository
    {
        public string CollectionName = "UserData";
        private readonly IMongoCollection<UserCredentials> UserDataCollection;

        public UserDataRepository(IMongoDatabase database)
        {
            this.UserDataCollection = database.GetCollection<UserCredentials>(this.CollectionName);
        }

        public void LoadUserData(IMongoDatabase database)
        {

            var collection = database.GetCollection<UserCredentials>(this.CollectionName);
            var nDocuments = collection.CountDocuments(FilterDefinition<UserCredentials>.Empty);

            if (nDocuments == 0)
            {
                UserCredentials userData = JsonConvert.DeserializeObject<Collection<UserCredentials>>(Resources.UserData).FirstOrDefault();
                collection.InsertOne(userData);
            }

        }

        public bool CheckUsernameExists(string userName)
        {
            var docFind = false;

            var nDocuments = this.UserDataCollection.Find(usr => usr.UserName == userName).CountDocuments();
             
            if (nDocuments == 1) docFind = true;

            else docFind = false;

            return docFind;

        }

        public string GetPasswordEncoded(string userName)
        {

            var loginCredentials = this.UserDataCollection.Find(usr => usr.UserName == userName).FirstOrDefault<UserCredentials>();

            return loginCredentials.Password;
        }

    }
}
