using Confluent.Kafka;
using System.Text.Json;
using WalletAPI.Application.DTOs;
using WalletAPI.Application.Services.Interfaces;
;

namespace WalletAPI.Application.Services.Implementations
{
    public class ProducerService : IProducerService
    {
        private readonly ILogger<ProducerService> _logger;
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;



        public ProducerService(ILogger<ProducerService> logger)
        {
            _logger = logger;
        }

        

    }
}   
