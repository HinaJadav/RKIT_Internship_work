/*using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
    [ApiController]
    public class HandleRoutesController : Controller
    {
        // get api simple route --> simple welcome massage


        // routing with one variable ->  student id

        // routing with 2 variables -> take student first name , & student last name, 

        // pass data into routing using query string -> eduction info 

        // one method with different unquie rout values --> any thing logical to replated to my current student management task 

        // one route gives to multiple methods --> error or exception handling in this case

        // token replacement : means access ssame resources(method) using controller name and routing -- [controller]/[action] or [action\/[controller] both way is valid -. take multiple student info name, id  

        // set one base route for entire controller

        // add one method which override existinf base url and creates own using[~/] symbol -- for show all student details

        // routing contraint
        // 1) type contraint into route
        // 2) min(number)
        // 3) max(nnumber)
        // 4) minlength(<length size>)
        // 5) length(<length size>)
        // 6) maxlength(<length size>)
        // 7) range(minvalue, maxValue)
        // 8) alpha - only for alphabatical character allow
        // 9) required
        // 10) gegex(expression)

        // less than 
        // string 
        // greater than 

    }
}*/

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
        /// Accessed via: GET api/handle-routes/welcome
        /// </summary>
        [HttpGet("welcome", Name = "GetWelcomeMessage")]
        public IActionResult WelcomeMessage()
        {
            return Ok("Welcome to the Student Management System!");
        }


        ///<summary>
        /// Routing with one variable for student ID.
        /// Accessed via: GET api/handle-routes/student/{id}
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
        /// Accessed via: GET api/handle-routes/student/{firstName}/{lastName}
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
        /// Accessed via: GET api/handle-routes/education?educationInfo=BSc Computer Science
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
        /// Accessed via: GET api/handle-routes/search or GET api/handle-routes/find
        /// </summary>
        [HttpGet("search")]
        [HttpGet("find")]
        public IActionResult SearchStudents()
        {
            return Ok(Students);
        }



        ///<summary>
        /// Token replacement for dynamic route handling.
        /// Accessed via: GET api/handle-routes/token/{action}/{controller}
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
