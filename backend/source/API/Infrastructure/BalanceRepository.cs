using System.Collections.ObjectModel;
using MongoDB.Driver;
using OriolOr.Maneko.API.Domain;
using OriolOr.Maneko.API.Infrastructure.Interfaces;
using OriolOr.Maneko.API.Infrastructure.DTOs;
using OriolOr.Maneko.API.Infrastructure.Mappers;
using OriolOr.Maneko.API.Properties;

namespace OriolOr.Maneko.API.Infrastructure
{
    public class BalanceRepository: IBalanceRepository
    {
        public string CollectionName => "Balances";

        public void LoadAccountData(IMongoDatabase database)
        {
            var collection = database.GetCollection<BalanceDTO>(this.CollectionName);
            var nDocuments = collection.CountDocuments(FilterDefinition<BalanceDTO>.Empty);

        }

        //TODO move to new collection
        //public double GetAccountCurrentBalance(IMongoDatabase database)
        //{
        //    var collection = database.GetCollection<Account>(this.CollectionName);

        //    var accDocument = collection.Find(FilterDefinition<Account>.Empty).FirstOrDefault();
        //    return accDocument.CurrentBalance;
        //}


        //public Collection<YearBalance> GetYearBalance(IMongoDatabase database)
        //{
        //   var collection = database.GetCollection<BalanceDTO>(this.CollectionName);

        //   //var accDocument = collection.Find(FilterDefinition<Account>.Empty).FirstOrDefault();
        //   // return accDocument.YearHistory;
        //}

        public void AddBalance(IMongoDatabase database, MonthBalance monthBalance)
        {
            var collection = database.GetCollection<BalanceDTO>(this.CollectionName);

            var balance = MonthBalanceMapper.ToDTO(monthBalance);
            collection.InsertOne(balance);
            //think about how to add this data (load year and then update it with new month ? )
        }

        //TODO move to new collection
        //private bool CheckAccountIdExists(IMongoDatabase database, string accountId)
        //{
        //    var collection = database.GetCollection<Account>(this.CollectionName);

        //    var accElements = collection.Find(acc => acc.AccountNumberId == accountId).Count();

        //    if (accElements == 0)
        //    {
        //        return false; 
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}
    }
}
