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
            new Student { EnrollmentID = 2, Name = "Ananya Iyer", Email = "ananya.iyer@example.com", ContactInformation = 9123456789, DateOfBirth = new DateTime(1999, 9, 20), Gender = "Female", Address = "56 Residency Road, Chennai", YearOfGraduation = 2023, StudentSeatType = "Self-Financed", FeesStatus = "UNPAID", DepartmentID = 2, IsActive = 1 },
            new Student { EnrollmentID = 3, Name = "Rohan Gupta", Email = "rohan.gupta@example.com", ContactInformation = 9123456783, DateOfBirth = new DateTime(2001, 3, 25), Gender = "Male", Address = "123 Nehru Nagar, Mumbai", YearOfGraduation = 2025, StudentSeatType = "GIA", FeesStatus = "PAID", DepartmentID = 3, IsActive = 1 },
            new Student { EnrollmentID = 4, Name = "Ishita Roy", Email = "ishita.roy@example.com", ContactInformation = 9988776655, DateOfBirth = new DateTime(2000, 2, 10), Gender = "Female", Address = "45 Salt Lake, Kolkata", YearOfGraduation = 2024, StudentSeatType = "Management", FeesStatus = "UNPAID", DepartmentID = 4, IsActive = 1 },
            new Student { EnrollmentID = 5, Name = "Aryan Verma", Email = "aryan.verma@example.com", ContactInformation = 9871234567, DateOfBirth = new DateTime(1998, 11, 15), Gender = "Male", Address = "23 Civil Lines, Lucknow", YearOfGraduation = 2022, StudentSeatType = "GIA", FeesStatus = "PAID", DepartmentID = 5, IsActive = 1 },
            new Student { EnrollmentID = 6, Name = "Sanya Mehta", Email = "sanya.mehta@example.com", ContactInformation = 8765432190, DateOfBirth = new DateTime(1997, 5, 5), Gender = "Female", Address = "78 Lajpat Nagar, Delhi", YearOfGraduation = 2021, StudentSeatType = "Self-Financed", FeesStatus = "UNPAID", DepartmentID = 1, IsActive = 1 },
            new Student { EnrollmentID = 7, Name = "Kabir Singh", Email = "kabir.singh@example.com", ContactInformation = 9123496789, DateOfBirth = new DateTime(1999, 7, 7), Gender = "Male", Address = null, YearOfGraduation = 2023, StudentSeatType = "GIA", FeesStatus = "PAID", DepartmentID = 5, IsActive = 1 },
            new Student { EnrollmentID = 8, Name = "Aditi Rao", Email = "aditi.rao@example.com", ContactInformation = 7654321987, DateOfBirth = null, Gender = "Female", Address = "34 Banjara Hills, Hyderabad", YearOfGraduation = 2025, StudentSeatType = "Management", FeesStatus = "UNPAID", DepartmentID = 4, IsActive = 1 },
            new Student { EnrollmentID = 9, Name = "Vihan Nair", Email = "vihan.nair@example.com", ContactInformation = 8765123498, DateOfBirth = new DateTime(2003, 12, 31), Gender = "Male", Address = "12 Model Town, Chandigarh", YearOfGraduation = 2026, StudentSeatType = "GIA", FeesStatus = "PAID", DepartmentID = 3, IsActive = 1 },
            new Student { EnrollmentID = 10, Name = "Priya Desai", Email = "priya.desai@example.com", ContactInformation = 9877896543, DateOfBirth = new DateTime(2004, 4, 18), Gender = "Female", Address = "67 Koregaon Park, Pune", YearOfGraduation = 2027, StudentSeatType = "Self-Financed", FeesStatus = "UNPAID", DepartmentID = 2, IsActive = 1 },
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
