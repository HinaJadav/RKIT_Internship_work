using Microsoft.AspNetCore.Mvc;

namespace BasicDemo.Controllers
{
    [ApiController]
    [Route("basicDemo/[action]")]
    public class BasicDemoController : ControllerBase
    {
       public string GetName()
        {
            return "Priyank";
        }

        public int GetRollNo()
        {
            return 1;
        }
    }
}
