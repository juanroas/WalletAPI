using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletAPI.Domain.Models;
using WalletAPI.Infrastructure.Data;

namespace WalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WalletController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wallet>>> GetWallets()
        {
            var wallets = await _context.Wallets.ToListAsync();
            return Ok("wallets");
        }
    }
}

