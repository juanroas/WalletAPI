using Confluent.Kafka;
using System.Text.Json;
using WalletAPI.Application.DTOs;
using WalletAPI.Application.Services.Interfaces;

namespace WalletAPI.Application.Services.Implementations
{
    public class KafkaConsumerService : IKafkaConsumerService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly string _topic;

        public KafkaConsumerService(IConfiguration configuration, ILogger<KafkaConsumerService> logger)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = "wallet-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _topic = configuration["Kafka:TransactionTopic"] ?? "transactions";
            _logger = logger;
        }

        public async Task ConsumeTransactionEventsAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    var transaction = JsonSerializer.Deserialize<TransactionDto>(consumeResult.Message.Value);
                    _logger.LogInformation($"Processing transaction: {transaction.Id}, Amount: {transaction.Amount}");
                    // Aqui podemos processar a transação (ex: atualizar saldo no banco)
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing transaction: {ex.Message}");
                }
            }
            _consumer.Close();
        }
    }
}
