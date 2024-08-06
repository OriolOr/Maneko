using Microsoft.Extensions.DependencyInjection;
using OriolOr.Maneko.API.Service.Interfaces;

namespace OriolOr.Maneko.API.Service.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBalanceService, BalanceService>()
                    .AddScoped<IUserCredentialsService, UserCredentialsService>();

            return services;
        }
    }
}
