using HTTPCachongInWebAPI;
using System;
using System.Web.Http;

namespace HTTPCachingInWebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for managing student-related operations.
    /// </summary>
    public class StudentsController : ApiController
    {
        /// <summary>
        /// Retrieves the list of students. If the data is available in the cache, it is returned from the cache;
        /// otherwise, the data is fetched and then cached for future requests.
        /// </summary>
        /// <returns>An <see cref="IHttpActionResult"/> containing the list of students.</returns>
        [HttpGet]
        [Route("api/students")]
        public IHttpActionResult GetStudents()
        {
            // Define a unique key for caching the students list
            string cacheKey = "StudentsCacheKey";  // Unique cache key for the students list

            // Try to get the data from the cache
            var cachedStudents = CacheHelper.Get(cacheKey);

            // If data is found in the cache, return it
            if (cachedStudents != null)
            {
                return Ok(cachedStudents); // Return the cached students list
            }

            // Simulate fetching data (e.g., from a database or an external data source)
            var students = new[]
            {
                new { Id = 1, Name = "Nahi", Age = 20 },
                new { Id = 2, Name = "Jahan", Age = 22 },
                new { Id = 3, Name = "Jimin", Age = 21 }
            };

            // Cache the fetched students list for 10 minutes
            CacheHelper.Set(cacheKey, students, TimeSpan.FromMinutes(10));

            // Return the fetched students list after caching it
            return Ok(students);
        }
    }
}
