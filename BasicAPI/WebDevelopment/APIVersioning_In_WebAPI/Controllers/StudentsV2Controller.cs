using APIVersioning_In_WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APIVersioning_In_WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing student data for version 2 of the API.
    /// </summary>
    // [RoutePrefix("api/v2/students")]
    public class StudentsV2Controller : ApiController
    {
        #region In-Memory Data

        // In-memory list of students with first and last names, and contact info
        private static List<StudentV2Model> students = new List<StudentV2Model>
        {
            new StudentV2Model { Id = 1, FirstName = "Amit", LastName = "Sharma", Marks = 85, ContactInfo = "amit.sharma@gmail.com" },
            new StudentV2Model { Id = 2, FirstName = "Priya", LastName = "Patel", Marks = 92, ContactInfo = "priya.patel@yahoo.com" },
            new StudentV2Model { Id = 3, FirstName = "Ravi", LastName = "Kumar", Marks = 78, ContactInfo = "ravi.kumar@outlook.com" },
            new StudentV2Model { Id = 4, FirstName = "Sneha", LastName = "Reddy", Marks = 88, ContactInfo = "sneha.reddy@hotmail.com" }
        };

        #endregion

        #region API Endpoints

        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>A list of all students.</returns>
        [HttpGet]
        // [Route("")]
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
        // [Route("{id:int}")]
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
