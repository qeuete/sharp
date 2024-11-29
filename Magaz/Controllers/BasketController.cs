using Microsoft.AspNetCore.Mvc;

namespace Magaz.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
