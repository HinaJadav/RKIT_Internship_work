using APIVersioning.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APIVersioning.Controllers
{
    /// <summary>
    /// Controller for managing student data for version 1 of the API.
    /// </summary>
    [RoutePrefix("api/v1/students")]
    public class StudentsV1Controller : ApiController
    {
        #region In-Memory Data

        // In-memory list of students with some Indian names
        private static List<StudentV1Model> students = new List<StudentV1Model>
        {
            new StudentV1Model { Id = 1, Name = "Amit Sharma", Marks = 85 },
            new StudentV1Model { Id = 2, Name = "Priya Patel", Marks = 92 },
            new StudentV1Model { Id = 3, Name = "Ravi Kumar", Marks = 78 },
            new StudentV1Model { Id = 4, Name = "Sneha Reddy", Marks = 88 }
        };

        #endregion

        #region API Endpoints

        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>A list of all students.</returns>
        [HttpGet]
        
        public IHttpActionResult GetAll()
        {
            return Ok(students);
        }

        /// <summary>
        /// Gets a student by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the student.</param>
        /// <returns>The student with the given ID, or not found.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        #endregion
    }
}