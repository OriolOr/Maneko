﻿using MongoDB.Driver;
using Newtonsoft.Json;
using OriolOr.Maneko.API.Domain;
using OriolOr.Maneko.API.Domain.IdentityManagement;
using OriolOr.Maneko.API.Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using System.Globalization;
using OriolOr.Maneko.API.Properties;

namespace OriolOr.Maneko.API.Infrastructure
{
    public class AccountDataRepository : IAccountDataRepository
    {
        public string CollectionName => "AccountData";

        public void LoadAccountData(IMongoDatabase database, UserCredentials userCredentials)
        {
            var collection = database.GetCollection<Account>(this.CollectionName);
            var nDocuments = collection.CountDocuments(FilterDefinition<Account>.Empty);
            var accDocumentLastUpdate = new DateTime();

            if (nDocuments != 0) accDocumentLastUpdate = collection.Find(FilterDefinition<Account>.Empty).FirstOrDefault().LastUpdate;
             
            if (accDocumentLastUpdate.Day != DateTime.Now.Day)
            {
                Account account = new Account();
                
                account.LastUpdate = DateTime.Now;
                account.CurrentBalance = 0;
                account.AccountNumberId = "AA0000000000000000000000";
                

                //CREATE OBJECT TO BE INSERED IN DATABASE 
                var newYearBalance = new YearBalance();

                newYearBalance.Year = DateTime.Now.Year;
                newYearBalance.MonthBalances = JsonConvert.DeserializeObject<Collection<MonthBalance>>(Resources.BankData);

                account.YearHistory.Add(newYearBalance);

                //INSERT TO DATABASE
                if (this.CheckAccountIdExists(database, account.AccountNumberId))
                {
                    //Update existing element in database 
                    collection.ReplaceOne(ac => ac.AccountNumberId == account.AccountNumberId, account);
                }
                else
                {
                    collection.InsertOne(account);
                }
            }
        }
        
        public double GetAccountCurrentBalance(IMongoDatabase database)
        {
            var collection = database.GetCollection<Account>(this.CollectionName);

            var accDocument = collection.Find(FilterDefinition<Account>.Empty).FirstOrDefault();
            return accDocument.CurrentBalance;
        }

        public Collection<YearBalance> GetYearBalance(IMongoDatabase database)
        {
            var collection = database.GetCollection<Account>(this.CollectionName);

            var accDocument = collection.Find(FilterDefinition<Account>.Empty).FirstOrDefault();
            return accDocument.YearHistory;
        }

        private bool CheckAccountIdExists(IMongoDatabase database, string accountId)
        {
            var collection = database.GetCollection<Account>(this.CollectionName);

            var accElements = collection.Find(acc => acc.AccountNumberId == accountId).Count();

            if (accElements == 0)
            {
                return false; 
            }
            else
            {
                return true;
            }

        }
    }
}
