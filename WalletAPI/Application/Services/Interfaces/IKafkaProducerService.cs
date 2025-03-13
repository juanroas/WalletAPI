using WalletAPI.Application.DTOs;

namespace WalletAPI.Application.Services.Interfaces
{
    public interface IKafkaProducerService
    {
        Task PublishTransactionEventAsync(TransactionDto transaction);
    }
}
