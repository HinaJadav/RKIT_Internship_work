using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Welcome to Home Page!");
        }

        public IActionResult About()
        {
            return Content("This is the About Us page.");
        }
    }
}
