using Microsoft.AspNetCore.Mvc;

namespace SneakerStorePage.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
