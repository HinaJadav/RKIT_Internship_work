using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;

namespace RoutingDemo.Controllers
{
    [ApiController]
    [Route("api")] // Set base route for the entire controller
    public class StudentController : ControllerBase
    {
        // In-memory student list to simulate a simple student management system
        private static List<Student> Students = new List<Student>
        {
            new Student { Id = 1, FirstName = "John", LastName = "Doe", EducationInfo = "BSc Computer Science" },
            new Student { Id = 2, FirstName = "Jane", LastName = "Smith", EducationInfo = "BA Economics" }
        };

        // Route name: use to generate a URL based on a specific route

        ///<summary>
        /// Simple route that returns a welcome message.
        /// Accessed via: GET api/welcome
        /// Name : used to assign a route name to the endpoint. 
        /// </summary>
        [HttpGet("welcome", Name = "GetWelcomeMessage")]
        public IActionResult WelcomeMessage()
        {
            return Ok("Welcome to the Student Management System!");
        }


        ///<summary>
        /// Routing with one variable for student ID.
        /// Accessed via: GET api/student/{id}
        /// </summary>
        [HttpGet("student/{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student not found.");
            }
            return Ok(student);
        }

        ///<summary>
        /// Routing with two variables for student first and last name.
        /// Accessed via: GET api/student/{firstName}/{lastName}
        /// </summary>
        [HttpGet("student/{firstName}/{lastName}")]
        public IActionResult GetStudentByName(string firstName, string lastName)
        {
            var student = Students.FirstOrDefault(s => s.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
            if (student == null)
            {
                return NotFound("Student not found.");
            }
            return Ok(student);
        }

        ///<summary>
        /// Passing data into routing using query string for education info.
        /// Accessed via: GET api/education?educationInfo=BSc Computer Science
        /// </summary>
        [HttpGet("education")]
        public IActionResult GetStudentByEducation([FromQuery] string educationInfo)
        {
            var students = Students.Where(s => s.EducationInfo.Contains(educationInfo, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!students.Any())
            {
                return NotFound("No students found with the specified education info.");
            }
            return Ok(students);
        }

        ///<summary>
        /// One method with different unique route values for student search.
        /// Accessed via: GET api/search or GET api/  /find
        /// </summary>
        [HttpGet("search")]
        [HttpGet("find")]
        public IActionResult SearchStudents()
        {
            return Ok(Students);
        }



        ///<summary>
        /// Token replacement for dynamic route handling.
        /// Accessed via: GET api/token/{action}/{controller}
        /// This method dynamically handles routes using controller and action tokens.
        /// </summary>
        [HttpGet("token/{action}/{controller}")]
        public IActionResult TokenReplacement(string action, string controller)
        {
            return Ok($"This route dynamically handles token replacement. Action: {action}, Controller: {controller}");
        }

        ///<summary>
        /// Method that overrides existing base URL and creates its own route.
        /// Accessed via: GET ~/showAllStudents
        /// This method demonstrates route overriding by creating a custom route for showing all students.
        /// </summary>
        [HttpGet("~/showAllStudents")]
        public IActionResult ShowAllStudents()
        {
            return Ok(Students);
        }
    }


}
