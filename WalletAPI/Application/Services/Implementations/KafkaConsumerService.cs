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
                var consumeResult = new ConsumeResult<string, string>();
                try
                {
                    consumeResult = _consumer.Consume(cancellationToken);
                    var mensagem = consumeResult.Message.Value;

                    _logger.LogInformation($"Mensagem recebida do Kafka: {mensagem}");

                    if (string.IsNullOrWhiteSpace(mensagem) || !mensagem.Trim().StartsWith("{"))
                    {
                        _logger.LogWarning($"Mensagem inválida recebida e ignorada: {mensagem}");
                        continue;
                    }

                    var transaction = JsonSerializer.Deserialize<TransactionDto>(mensagem);

                    if (transaction == null || string.IsNullOrWhiteSpace(transaction.Id.ToString()) || transaction.Amount <= 0)
                    {
                        _logger.LogWarning("Transação recebida com valores inválidos. Ignorando.");
                        continue;
                    }

                    _logger.LogInformation($"Processing transaction: {transaction.Id}, Amount: {transaction.Amount}");
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError($"Erro ao desserializar JSON: {jsonEx.Message}. Mensagem ignorada: {consumeResult?.Message?.Value}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro inesperado ao processar transação: {ex.Message}");
                }
            }
            _consumer.Close();
        }
    }
}
