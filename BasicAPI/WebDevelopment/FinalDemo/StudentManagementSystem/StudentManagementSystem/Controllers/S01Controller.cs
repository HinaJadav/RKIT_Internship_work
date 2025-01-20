using StudentManagementSystem.BL;
using StudentManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StudentManagementSystem.Controllers
{
    [Route("api/s01")]
    public class S01Controller : ApiController
    {
        private readonly S01Service _service;

        public S01Controller(S01Service service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetAllStudent()
        {
            List<YMS01> data = _service.GetAll();

            if (data == null || !data.Any())  // Also checks if data is empty
            {
                return NotFound();  // Returns 404 Not Found if data is null or empty
            }

            return Ok(data);  // Returns 200 OK with the serialized JSON data automatically
        }


    }
}