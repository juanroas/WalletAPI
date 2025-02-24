using Microsoft.AspNetCore.Mvc;

namespace WalletAPI.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
