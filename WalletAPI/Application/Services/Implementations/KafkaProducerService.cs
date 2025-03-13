using Confluent.Kafka;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WalletAPI.Application.DTOs;
using WalletAPI.Application.Services.Interfaces;

namespace WalletAPI.Application.Services.Implementations
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public KafkaProducerService(IConfiguration configuration)
        {
            var config = new ProducerConfig { BootstrapServers = configuration["Kafka:BootstrapServers"] };
            _producer = new ProducerBuilder<string, string>(config).Build();
            _topic = configuration["Kafka:TransactionTopic"] ?? "transactions";
        }

        public async Task PublishTransactionEventAsync(TransactionDto transaction)
        {
            var message = new Message<string, string>
            {
                Key = transaction.Id.ToString(),
                Value = JsonSerializer.Serialize(transaction)
            };
            await _producer.ProduceAsync(_topic, message);
        }
    }
}
