using Microsoft.AspNetCore.Mvc;

namespace CowRationWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
