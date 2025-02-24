using Microsoft.AspNetCore.Mvc;

namespace WalletAPI.Controllers
{
    public class WalletController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
