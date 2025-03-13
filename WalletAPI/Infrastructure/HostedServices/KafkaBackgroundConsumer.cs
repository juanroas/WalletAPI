using WalletAPI.Application.Services.Interfaces;

namespace WalletAPI.Infrastructure.HostedServices
{
    public class KafkaBackgroundConsumer : BackgroundService
    {
        private readonly IKafkaConsumerService _kafkaConsumerService;

        public KafkaBackgroundConsumer(IKafkaConsumerService kafkaConsumerService)
        {
            _kafkaConsumerService = kafkaConsumerService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _kafkaConsumerService.ConsumeTransactionEventsAsync(stoppingToken);
        }
    }
}
