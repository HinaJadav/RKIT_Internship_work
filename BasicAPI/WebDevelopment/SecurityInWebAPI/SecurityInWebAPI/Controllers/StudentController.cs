using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using System;
using SecurityInWebAPI.Models;
using SecurityInWebAPI.Handlers;

namespace SecurityInWebAPI.Controllers
{
    /// <summary>
    /// API controller for managing student data.
    /// Provides methods to retrieve, add, and delete student information.
    /// Data is persisted in a JSON file for storage.
    /// </summary>
    [RoutePrefix("api/students")]
   
    public class StudentController : ApiController
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "students.json");

        /// <summary>
        /// Loads student data from the JSON file.
        /// If the file does not exist, initializes it with an empty list.
        /// </summary>
        /// <returns>A list of students.</returns>
        private List<Student> LoadStudentsFromFile()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]"); // Initialize with an empty list if file doesn't exist
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
        }

        /// <summary>
        /// Saves the student data to the JSON file.
        /// </summary>
        /// <param name="students">The list of students to save.</param>
        private void SaveStudentsToFile(List<Student> students)
        {
            var json = JsonConvert.SerializeObject(students, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // Add a Login method to authenticate users and return a JWT token
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Authenticate(LoginModel loginModel)
        {
            // Replace this with actual user authentication logic (e.g., database lookup)
            if (loginModel.Username == "admin" && loginModel.Password == "password")  // Dummy check
            {
                var token = JwtTokenManager.GenerateToken(loginModel.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        /// <summary>
        /// Retrieves all students from the JSON file.
        /// </summary>
        /// <returns>A collection of all students.</returns>
        [HttpGet]
        [Route("all")]
        public IEnumerable<Student> GetAllStudents()
        {
            return LoadStudentsFromFile();
        }

        /// <summary>
        /// Retrieves a specific student by their enrollment ID.
        /// </summary>
        /// <param name="id">The enrollment ID of the student.</param>
        /// <returns>The student with the specified enrollment ID or a NotFound response if not found.</returns>
        [HttpGet]
        [Authorize]
        [Route("{id:int}")]
        public IHttpActionResult GetStudentByEnrollmentID(int id)
        {
            var students = LoadStudentsFromFile();
            var student = students.FirstOrDefault(s => s.EnrollmentID == id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        /// <summary>
        /// Adds a new student to the JSON file.
        /// </summary>
        /// <param name="newStudent">The new student to add.</param>
        /// <returns>A Created response with the added student or a BadRequest response for invalid data.</returns>
        [HttpPost]
        [Authorize(Users = "Nahi")]
        [Route("")]
        public IHttpActionResult AddNewStudent(Student newStudent)
        {
            var students = LoadStudentsFromFile();

            if (newStudent == null || students.Any(s => s.EnrollmentID == newStudent.EnrollmentID))
                return BadRequest("Invalid or duplicate student data.");

            students.Add(newStudent);
            SaveStudentsToFile(students);

            return Created($"api/students/{newStudent.EnrollmentID}", newStudent);
        }

        /// <summary>
        /// Deletes a student by their enrollment ID from the JSON file.
        /// </summary>
        /// <param name="id">The enrollment ID of the student to delete.</param>
        /// <returns>A response indicating success or failure of the deletion.</returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public IHttpActionResult DeleteStudent(int id)
        {
            var students = LoadStudentsFromFile();
            var student = students.FirstOrDefault(s => s.EnrollmentID == id);
            if (student == null) return NotFound();

            students.Remove(student);
            SaveStudentsToFile(students);

            return Ok($"Deleted student with ID {id}.");
        }
    }
}