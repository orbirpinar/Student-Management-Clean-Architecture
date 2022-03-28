using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}