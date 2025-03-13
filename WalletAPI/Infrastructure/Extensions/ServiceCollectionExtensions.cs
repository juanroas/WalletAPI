using StackExchange.Redis;
using WalletAPI.Application.Services.Implementations;
using WalletAPI.Application.Services.Interfaces;


namespace WalletAPI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string redisConnection)
        {
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
            services.AddScoped<IRedisService, RedisService>();
            return services;
        }
    }
}
