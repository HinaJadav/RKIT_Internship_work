using CORSInWebAPISecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CORSInWebAPISecurity.Controllers
{
    /// <summary>
    /// Handles operations related to student information.
    /// </summary>
    /// <remarks>
    /// CORS is enabled at the controller level for specific origins, headers, and methods.
    /// </remarks>
    [EnableCors(origins: "https://localhost:44324", headers: "Authorization, Content-Type", methods: "GET")]
    public class StudentController : ApiController
    {
        /// <summary>
        /// In-memory list of students.
        /// </summary>
        private List<Student> students = new List<Student>
        {
            new Student { EnrollmentID = 1, Name = "Aarav Sharma", Email = "aarav.sharma@example.com", ContactInformation = 9876543210, DateOfBirth = new DateTime(2000, 6, 15), Gender = "Male", Address = "12 MG Road, Bengaluru", YearOfGraduation = 2024, StudentSeatType = "GIA", FeesStatus = "PAID", DepartmentID = 1, IsActive = 1 },
            // Additional student records...
        };

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns>
        /// A list of all students if available, or a 404 Not Found response if the list is empty.
        /// </returns>
        [HttpGet]
        [EnableCors(origins: "https://localhost:44324", headers: "*", methods: "GET")]
        [Route("api/students/all")]
        public IHttpActionResult GetAllStudents()
        {
            if (students.Count == 0)
            {
                return NotFound(); // Returns 404 if no students found
            }
            return Ok(students); // Returns 200 OK with the list of students
        }

        /// <summary>
        /// Retrieves a student by their Enrollment ID.
        /// </summary>
        /// <param name="enrollmentID">The enrollment ID of the student.</param>
        /// <returns>
        /// The details of the student if found, or a 404 Not Found response if no such student exists.
        /// </returns>
        [HttpGet]
        [EnableCors(origins: "https://localhost:44324", headers: "*", methods: "GET")]
        [Route("api/students/enrollmentID/{enrollmentID}")]
        public IHttpActionResult GetStudentByEnrollmentID(int enrollmentID)
        {
            Student student = students.FirstOrDefault(s => s.EnrollmentID == enrollmentID);
            if (student == null)
            {
                return NotFound(); // Returns 404 if no students found
            }
            return Ok(student); // Returns 200 OK with the student details
        }

        /// <summary>
        /// Adds a new student to the list.
        /// </summary>
        /// <param name="newStudent">The student object to be added.</param>
        /// <returns>
        /// A 201 Created response with the URI of the new student, or an error response if the input is invalid or the student already exists.
        /// </returns>
        [HttpPost]
        public IHttpActionResult AddNewStudent(Student newStudent)
        {
            if (newStudent == null)
            {
                return BadRequest("Invalid student data."); // Returns 400 for bad request
            }

            if (students.Any(s => s.EnrollmentID == newStudent.EnrollmentID))
            {
                return Conflict(); // Returns 409 if the student already exists
            }

            students.Add(newStudent);
            return Created($"api/students/{newStudent.EnrollmentID}", newStudent); // Returns 201 Created with the URI of the new student
        }

        /// <summary>
        /// Updates the details of an existing student.
        /// </summary>
        /// <param name="id">The enrollment ID of the student to be updated.</param>
        /// <param name="updatedStudent">The updated student object.</param>
        /// <returns>
        /// A 200 OK response with the updated student details, or a 404 Not Found response if the student does not exist.
        /// </returns>
        [HttpPut]
        [Route("api/students/{id}")]
        public IHttpActionResult UpdateStudent(int id, Student updatedStudent)
        {
            Student existingStudent = students.FirstOrDefault(s => s.EnrollmentID == id);
            if (existingStudent == null)
            {
                return NotFound(); // Returns 404 if no students found
            }

            // Update the student's details
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Email = updatedStudent.Email;
            existingStudent.ContactInformation = updatedStudent.ContactInformation;
            existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
            existingStudent.Gender = updatedStudent.Gender;
            existingStudent.Address = updatedStudent.Address;
            existingStudent.YearOfGraduation = updatedStudent.YearOfGraduation;
            existingStudent.StudentSeatType = updatedStudent.StudentSeatType;
            existingStudent.FeesStatus = updatedStudent.FeesStatus;
            existingStudent.DepartmentID = updatedStudent.DepartmentID;
            existingStudent.IsActive = updatedStudent.IsActive;

            return Ok(existingStudent); // Returns 200 OK after successfully updating the student
        }

        /// <summary>
        /// Deletes a student by their Enrollment ID.
        /// </summary>
        /// <param name="id">The enrollment ID of the student to be deleted.</param>
        /// <returns>
        /// A 200 OK response with a success message, or a 404 Not Found response if the student does not exist.
        /// </returns>
        [HttpDelete]
        [Route("api/students/{id}")]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student studentToDelete = students.FirstOrDefault(s => s.EnrollmentID == id);
            if (studentToDelete == null)
            {
                return NotFound(); // Returns 404 if student is not found
            }

            students.Remove(studentToDelete);
            return Ok($"Student with EnrollmentID {id} has been deleted."); // Returns 200 OK with a success message
        }
    }
}
