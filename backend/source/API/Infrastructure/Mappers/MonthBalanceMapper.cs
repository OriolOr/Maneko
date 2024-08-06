using OriolOr.Maneko.API.Domain;
using OriolOr.Maneko.API.Infrastructure.DTOs;

namespace OriolOr.Maneko.API.Infrastructure.Mappers;

public static class MonthBalanceMapper
{
    public static BalanceDTO ToDTO(MonthBalance monthBalance){
        
        return new BalanceDTO()
        {
            Month = monthBalance.Month,
            InitialBalance = monthBalance.InitialBalance,
            FinalBalance = monthBalance.FinalBalance,
            UserId = Guid.NewGuid().ToString(),
            Year = DateTime.Now.Year
        };
    }
}