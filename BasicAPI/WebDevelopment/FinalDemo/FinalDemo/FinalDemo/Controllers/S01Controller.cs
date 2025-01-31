using FinalDemo.BL;
using FinalDemo.Models;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Runtime.Caching;
using System.Web;
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



        /// <summary>
        /// Allows a user to sign in by validating their username and password. 
        /// Returns a JWT token if credentials are correct.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ApiVersion("1.0")]
        [Route("login")]
        public IHttpActionResult Signin([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (user.Username == "admin" && user.Password == "password")
            {
                // Generate and return JWT token
                var token = JwtHelper.GenerateToken(user.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        /// <summary>
        /// Gets all student data (API version 1.0). Data is cached for 60 minutes.
        /// </summary>
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
                _cache.Set(cacheKey, data, DateTimeOffset.Now.AddMinutes(60));

                return Ok(new { message = "This is API version 1.0", data = data });
            }

            return Ok(new { message = "Cache hit - This is API version 1.0", data = cachedData });
        }

        /// <summary>
        /// Gets all student data (API version 2.0). Data is cached for 60 seconds.
        /// </summary>
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

        /// <summary>
        /// Gets student data by ID (API version 1.0). Data is cached for 60 seconds.
        /// </summary>
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

        /// <summary>
        /// Adds a new student (API version 1.0).
        /// </summary>
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

        /// <summary>
        /// Updates a student by ID (API version 1.0). JWT is required for authorization.
        /// </summary>
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

        /// <summary>
        /// Deletes a student by ID (API version 1.0). JWT is required for authorization.
        /// </summary>
        [HttpDelete, Route("{id:int}")]
        [ApiVersion("1.0")]
        [JwtAuthorizor]
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
