namespace WalletAPI.Application.Services.Interfaces
{
    public interface IKafkaConsumerService
    {
        Task ConsumeTransactionEventsAsync(CancellationToken cancellationToken);
    }
}
