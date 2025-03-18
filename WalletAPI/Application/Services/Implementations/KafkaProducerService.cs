using Confluent.Kafka;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WalletAPI.Application.DTOs;
using WalletAPI.Application.Services.Interfaces;

namespace WalletAPI.Application.Services.Implementations
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly ILogger<KafkaProducerService> _logger;
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public KafkaProducerService(IConfiguration configuration, ILogger<KafkaProducerService> logger)
        {
            var config = new ProducerConfig { BootstrapServers = configuration["Kafka:BootstrapServers"] };
            _producer = new ProducerBuilder<string, string>(config).Build();
            _topic = configuration["Kafka:TransactionTopic"] ?? "transactions";
             _logger = logger;
        }

        public async Task PublishTransactionEventAsync(TransactionDto transaction)
        {
            
           try
            {
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var message = JsonSerializer.Serialize(transaction, options);

                _logger.LogInformation($"Enviando mensagem Kafka: {message}");

                if (string.IsNullOrWhiteSpace(transaction.Id.ToString()) || transaction.Amount <= 0)
                {
                    _logger.LogWarning("Tentando enviar transação inválida!");
                    return;
                }

                await _producer.ProduceAsync(_topic, new Message<string, string>
                {
                    Key = transaction.Id.ToString(),
                    Value = message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao publicar evento Kafka: {ex.Message}");
            }
        }
    }
}
