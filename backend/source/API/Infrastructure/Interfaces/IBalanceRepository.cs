using MongoDB.Driver;
using OriolOr.Maneko.API.Domain;

namespace OriolOr.Maneko.API.Infrastructure.Interfaces;

public interface IBalanceRepository
{
    public void AddBalance(IMongoDatabase database, MonthBalance balance);
}