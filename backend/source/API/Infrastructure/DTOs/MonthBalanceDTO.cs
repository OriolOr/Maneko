using OriolOr.Maneko.API.Domain;

namespace OriolOr.Maneko.API.Infrastructure.DTOs;

public class BalanceDTO
{
    public string Month;
    public float InitialBalance;
    public float FinalBalance;
    public string UserId;
    public int Year;
}