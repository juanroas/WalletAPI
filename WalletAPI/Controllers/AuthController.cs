using Microsoft.AspNetCore.Mvc;

namespace WalletAPI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
