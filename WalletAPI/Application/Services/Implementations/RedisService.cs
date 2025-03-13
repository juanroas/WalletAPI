using StackExchange.Redis;
using System.Text.Json;
using WalletAPI.Application.DTOs;
using WalletAPI.Application.Services.Interfaces;

namespace WalletAPI.Application.Services.Implementations
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _database;

        public RedisService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<decimal?> GetBalanceAsync(Guid userId)
        {
            var balance = await _database.StringGetAsync($"wallet:balance:{userId}");
            return balance.HasValue ? (decimal?)decimal.Parse(balance) : null;
        }

        public async Task SetBalanceAsync(Guid userId, decimal balance, TimeSpan expiration)
        {
            await _database.StringSetAsync($"wallet:balance:{userId}", balance.ToString(), expiration);
        }

        public async Task<List<TransactionDto>> GetTransactionsAsync(Guid userId)
        {
            var transactionsJson = await _database.StringGetAsync($"wallet:transactions:{userId}");
            return transactionsJson.HasValue
                ? JsonSerializer.Deserialize<List<TransactionDto>>(transactionsJson)
                : new List<TransactionDto>();
        }

        public async Task SetTransactionsAsync(Guid userId, List<TransactionDto> transactions, TimeSpan expiration)
        {
            var transactionsJson = JsonSerializer.Serialize(transactions);
            await _database.StringSetAsync($"wallet:transactions:{userId}", transactionsJson, expiration);
        }
    }
}