using Microsoft.AspNetCore.Mvc;


namespace ExceptionHandlingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("throw")]
        public IActionResult ThrowException()
        {
            HttpContext.Response.Cookies.Append("TestCookie", "CookieValue");
            throw new InvalidOperationException("This is a test exception");
        }
    }
}
