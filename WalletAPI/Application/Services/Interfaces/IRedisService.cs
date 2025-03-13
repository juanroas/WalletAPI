using WalletAPI.Application.DTOs;

namespace WalletAPI.Application.Services.Interfaces
{
public interface IRedisService
{
    Task<decimal?> GetBalanceAsync(Guid userId);
    Task SetBalanceAsync(Guid userId, decimal balance, TimeSpan expiration);
    Task<List<TransactionDto>> GetTransactionsAsync(Guid userId);
    Task SetTransactionsAsync(Guid userId, List<TransactionDto> transactions, TimeSpan expiration);
}
}