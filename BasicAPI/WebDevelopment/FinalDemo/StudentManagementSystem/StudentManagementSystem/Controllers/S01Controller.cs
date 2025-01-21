using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Http;
using Microsoft.Web.Http;
using StudentManagementSystem.BL;
using StudentManagementSystem.Models;
using StudentManagementSystem.Utilities;

namespace StudentManagementSystem.Controllers
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

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] User userModel)
        {
            // Authenticate the user
            if (userModel == null || string.IsNullOrEmpty(userModel.Username) || string.IsNullOrEmpty(userModel.Password))
            {
                return BadRequest("Invalid login request.");
            }

            // Assume you authenticate the user here
            if (_service.AuthenticateUser(userModel.Username, userModel.Password))
            {
                // Generate JWT token
                string token = JwtHelper.GenerateJwtToken(userModel.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }


        [HttpGet]
        [ApiVersion("1.0")]
        [Route("getAll")]
        public IHttpActionResult GetAllStudentV1()
        {
            string cacheKey = "AllStudentsV1"; // Unique cache key
            var cachedData = _cache.Get(cacheKey) as List<YMS01>;

            if (cachedData == null)
            {
                // If data is not cached, fetch it from the service
                List<YMS01> data = _service.GetAll();
                if (data == null || !data.Any())
                {
                    return NotFound();
                }

                // Store the fetched data in the cache for 60 seconds
                _cache.Set(cacheKey, data, DateTimeOffset.Now.AddSeconds(60));

                return Ok(new { message = "This is API version 1.0", data = data });
            }

            // If data is cached, return cached data
            return Ok(new { message = "Cache hit - This is API version 1.0", data = cachedData });
        }

        [HttpGet]
        [ApiVersion("2.0")]
        [Route("getAll")]
        public IHttpActionResult GetAllStudentV2()
        {
            string cacheKey = "AllStudentsV2"; // Unique cache key
            var cachedData = _cache.Get(cacheKey) as List<YMS01>;

            if (cachedData == null)
            {
                // If data is not cached, fetch it from the service
                List<YMS01> data = _service.GetAll();
                if (data == null || !data.Any())
                {
                    return NotFound();
                }

                // Store the fetched data in the cache for 60 seconds
                _cache.Set(cacheKey, data, DateTimeOffset.Now.AddSeconds(60));

                return Ok(new { message = "This is API version 2.0", data = data });
            }

            // If data is cached, return cached data
            return Ok(new { message = "Cache hit - This is API version 2.0", data = cachedData });
        }

        [HttpGet, Route("{id:int}")]
        [ApiVersion("1.0")]
        public IHttpActionResult GetStudentById(int id)
        {
            string cacheKey = $"StudentById_{id}"; // Unique cache key based on ID
            var cachedData = _cache.Get(cacheKey) as YMS01;

            if (cachedData == null)
            {
                // If data is not cached, fetch it from the service
                YMS01 data = _service.GetDataById(id);
                if (data == null)
                {
                    return NotFound();
                }

                // Store the fetched data in the cache for 60 seconds
                _cache.Set(cacheKey, data, DateTimeOffset.Now.AddSeconds(60));

                return Ok(data);
            }

            // If data is cached, return cached data
            return Ok(cachedData);
        }

        [HttpPost]
        [Authorize]
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

        [HttpPut, Route("{id:int}")]
        [Authorize]
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

        [HttpDelete, Route("{id:int}")]
        [Authorize]
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
