using Microsoft.Extensions.DependencyInjection;
using OriolOr.Maneko.API.Infrastructure;
using OriolOr.Maneko.API.Infrastructure.Interfaces;

namespace OriolOr.Maneko.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IBalanceRepository, BalanceRepository>()
                    .AddSingleton<IUserDataRepository, UserDataRepository>();
            return services;
        }
    }
}