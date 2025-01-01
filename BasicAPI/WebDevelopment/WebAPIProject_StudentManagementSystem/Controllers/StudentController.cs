using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIProject_StudentManagementSystem.Models;

namespace WebAPIProject_StudentManagementSystem.Controllers
{
    public class StudentController : ApiController
    {
        Student[] students = new Student[]
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

        // Get API
        [HttpGet]
        [Route("api/students/all")]
        public IEnumerable<Student> GetAllStudents()
        {
            return students;
        }

        // Get perticuler student by their enrollment ID
        [HttpGet]
        [Route("api/students/{id}")]
        public IHttpActionResult GetStudentByEnrollmentID(int enrollmentID)
        {
            // Retrieve the student by EnrollmentID
            Student student = students.FirstOrDefault(student1 => student1.EnrollmentID == enrollmentID);

            // If the student is not found, return a 404 Not Found
            if (student == null)
            {
                return NotFound();
            }

            // Return the found student with a 200 OK status
            return Ok(student);
        }


        // Post api
        [HttpPost]
        public IHttpActionResult AddNewStudent(Student newStudent)
        {
            // Validate the input
            if (newStudent == null)
            {
                return BadRequest("Invalid student data.");
            }

            if (students.Any(s => s.EnrollmentID == newStudent.EnrollmentID))
            {
                return BadRequest("A student with same enrollmentID is already exist.");
            }

            // Add the student into memory-list
            students.Add(newStudent);

            return Created($"api/students/{newStudent.EnrollmentID}", newStudent);
        }


    }
}
