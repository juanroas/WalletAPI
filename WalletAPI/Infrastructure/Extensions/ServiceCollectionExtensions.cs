using StackExchange.Redis;
using WalletAPI.Application.Services.Implementations;
using WalletAPI.Application.Services.Interfaces;
using WalletAPI.Infrastructure.HostedServices;


namespace WalletAPI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string redisConnection, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
            services.AddScoped<IRedisService, RedisService>();

            services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
            services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();

            services.AddHostedService<KafkaBackgroundConsumer>();

            return services;
        }
    }
}
