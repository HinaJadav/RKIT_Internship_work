using Microsoft.Web.Http;
using FinalDemo.BL;
using FinalDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Http;

namespace FinalDemo.Controllers
{
    [RoutePrefix("api/s01")]
    public class S01Controller : ApiController
    {
        private readonly S01Service _service;
        private static ObjectCache _cache = MemoryCache.Default;

        public S01Controller()
        {
            _service = new S01Service();
        }



        // Public Endpoint for all students (no JWT needed)
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("getAll")]
        public IHttpActionResult GetAllStudentV1()
        {
            string cacheKey = "AllStudentsV1";
            var cachedData = _cache.Get(cacheKey) as List<YMS01>;

            if (cachedData == null)
            {
                List<YMS01> data = _service.GetAll();
                if (data == null || !data.Any())
                {
                    return NotFound();
                }
                _cache.Set(cacheKey, data, DateTimeOffset.Now.AddSeconds(60));

                return Ok(new { message = "This is API version 1.0", data = data });
            }

            return Ok(new { message = "Cache hit - This is API version 1.0", data = cachedData });
        }

        [HttpGet]
        [ApiVersion("2.0")]
        [Route("getAll")]
        public IHttpActionResult GetAllStudentV2()
        {
            string cacheKey = "AllStudentsV2";
            var cachedData = _cache.Get(cacheKey) as List<YMS01>;

            if (cachedData == null)
            {
                List<YMS01> data = _service.GetAll();
                if (data == null || !data.Any())
                {
                    return NotFound();
                }
                _cache.Set(cacheKey, data, DateTimeOffset.Now.AddSeconds(60));

                return Ok(new { message = "This is API version 2.0", data = data });
            }

            return Ok(new { message = "Cache hit - This is API version 2.0", data = cachedData });
        }

        // Public Endpoint to get student by Id (no JWT needed)
        [HttpGet, Route("{id:int}")]
        [ApiVersion("1.0")]
        public IHttpActionResult GetStudentById(int id)
        {
            string cacheKey = $"StudentById_{id}";
            var cachedData = _cache.Get(cacheKey) as YMS01;

            if (cachedData == null)
            {
                YMS01 data = _service.GetDataById(id);
                if (data == null)
                {
                    return NotFound();
                }
                _cache.Set(cacheKey, data, DateTimeOffset.Now.AddSeconds(60));

                return Ok(data);
            }

            return Ok(cachedData);
        }

        // Public Endpoint to add a student (no JWT needed)
        [HttpPost]
        [Route("addAll")]
        [ApiVersion("1.0")]
        public IHttpActionResult AddStudent([FromBody] YMS01 newStudent)
        {
            if (newStudent == null)
            {
                return BadRequest("Invalid student data.");
            }

            _service.Add(newStudent);
            return Ok("Student added successfully.");
        }

        // Private Endpoint to update student (JWT required)
        [HttpPut, Route("{id:int}")]
        [ApiVersion("1.0")]
        public IHttpActionResult UpdateStudent(int id, [FromBody] YMS01 updatedStudent)
        {
            if (updatedStudent == null)
            {
                return BadRequest("Invalid student data.");
            }

            bool isUpdated = _service.Update(id, updatedStudent);

            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok("Student updated successfully.");
        }

        // Private Endpoint to delete student (JWT required)
        [HttpDelete, Route("{id:int}")]
        [ApiVersion("1.0")]
        public IHttpActionResult DeleteStudent(int id)
        {
            var isDeleted = _service.Delete(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok("Student deleted successfully.");
        }
    }
}
