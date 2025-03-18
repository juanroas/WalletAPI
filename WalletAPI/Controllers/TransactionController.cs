using Microsoft.AspNetCore.Mvc;
using WalletAPI.Application.DTOs;
using WalletAPI.Application.Services.Interfaces;

namespace WalletAPI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IKafkaProducerService _kafkaProducerService;

        public TransactionController(IKafkaProducerService kafkaProducerService)
        {
            _kafkaProducerService = kafkaProducerService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendTransaction([FromBody] TransactionDto transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Dados inválidos.");
            }

            await _kafkaProducerService.PublishTransactionEventAsync(transaction);

            return Ok(new { message = "Transação enviada para o Kafka com sucesso!" });
        }
    }
}
