using MongoDB.Driver;
using OriolOr.Maneko.API.Domain;
using OriolOr.Maneko.API.Domain.IdentityManagement;
using System.Collections.ObjectModel;

namespace OriolOr.Maneko.API.Infrastructure.Interfaces
{
    public interface IAccountDataRepository
    {
        public string CollectionName { get;}
        public void LoadAccountData(IMongoDatabase database, UserCredentials userCredentials);
        public double GetAccountCurrentBalance(IMongoDatabase database);
        public Collection<YearBalance> GetYearBalance(IMongoDatabase database);
        public void AddMonthBalance(IMongoDatabase database, MonthBalance monthBalance);
    }
}
