using WalletAPI.Application.DTOs;

namespace WalletAPI.Application.Services.Interfaces
{
    public interface IProducerService
    {
        Task PublishTransactionEventAsync(ProducerDto producerDTO);
    }
}
