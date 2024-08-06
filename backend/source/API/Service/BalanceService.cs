using OriolOr.Maneko.API.Infrastructure;
using OriolOr.Maneko.API.Infrastructure.Interfaces;
using OriolOr.Maneko.API.Domain;
using OriolOr.Maneko.API.Service.Interfaces;
using System.Collections.ObjectModel;

namespace OriolOr.Maneko.API.Service
{
    public class BalanceService : IBalanceService
    {
        public IBalanceRepository BalanceRepository;
        


        public BalanceService(IBalanceRepository balanceRepository) {
            this.BalanceRepository = balanceRepository;
        }

        public void AddMonthBalanceToDb(MonthBalance monthBalance)
            => BalanceRepository.AddBalance(MongoDbConfigurator.DataBase, monthBalance);

        public bool ValidateMonth(string month)
        {
           return Enum.TryParse<MonthEnum>(month, true , out _);
        }
    }
}
