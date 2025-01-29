using Microsoft.AspNetCore.Mvc;

namespace ActionResultDemo.Controllers
{
    public class HomeController : Controller
    {
        // IActionResult example
        public IActionResult Index()
        {
            return View(); // Returns the default view for Index
        }

        // ActionResult example
        public ActionResult About()
        {
            return View(); // Returns the default view for About
        }

        // ContentResult example
        public ContentResult ContentResultExample()
        {
            return Content("I am ContentResult", "text/plain"); // Returning plain text as content
        }

        // EmptyResult example
        public EmptyResult EmptyResultExample()
        {
            return new EmptyResult(); // Returning empty result
        }

        // JsonResult example
        public JsonResult JsonResultExample()
        {
            var person = new { Name = "Farhan Ahmed", Age = 30 };
            return Json(person); // Returning JSON data
        }

        // PartialViewResult example
        /*public PartialViewResult PartialViewResultExample()
        {
            return PartialView("_PartialView"); // Rendering a partial view
        }*/

        // ViewResult example
        public ViewResult ViewResultExample()
        {
            return View("About", "Home"); // Rendering the About view from Home controller
        }

        // ViewComponentResult example
        /*public ViewComponent ViewComponentResultExample()
        {
            return ViewComponent("MyComponent"); // Rendering a view component
        }*/
    }
}
