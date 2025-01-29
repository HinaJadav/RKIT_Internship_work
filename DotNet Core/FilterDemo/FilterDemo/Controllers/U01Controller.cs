using FilterDemo.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FilterDemo.Controllers
{
    public class U01Controller : Controller
    {
        [Route("api/secure")]
        [ApiController]
        public class SecureController : ControllerBase
        {
            [HttpGet("admin")]
            [AuthorizationFilter("Admin")]
            public IActionResult AdminEndpoint()
            {
                return Ok("Hello, Admin! You have access.");
            }

            [HttpGet("manager")]
            [AuthorizationFilter("Manager")]
            public IActionResult ManagerEndpoint()
            {
                return Ok("Hello, Manager! You have access.");
            }
        }
    }
}
