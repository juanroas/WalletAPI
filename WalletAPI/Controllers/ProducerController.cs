using Microsoft.AspNetCore.Mvc;
using WalletAPI.Application.DTOs;
using WalletAPI.Application.Services.Interfaces;

namespace WalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducerController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendProducer([FromBody] ProducerDto producer)
        {
            if (producer == null)
            {
                return BadRequest("data invalid");
            }

            await _producerService.PublishTransactionEventAsync(producer);

            return Ok(new {message = "Transaction sent to Kafka with success"});
        }
    }
}
