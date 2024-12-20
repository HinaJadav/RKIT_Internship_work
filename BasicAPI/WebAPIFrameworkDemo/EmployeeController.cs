using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebAPIFrameworkDemo
{
    /// <summary>
    /// Represents an Employee with properties like Id, Name, Description, and Salary.
    /// </summary>
    public class Employee
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the job description of the employee.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        public int Salary { get; set; }

        #endregion
    }

    /// <summary>
    /// API Controller to manage employee data.
    /// </summary>
    public class EmployeeController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// List of employees to simulate a data store.
        /// </summary>
        private List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Priyank Jadav", Description = "Manager", Salary = 60000 },
            new Employee { Id = 2, Name = "Madhu Koria", Description = "HR", Salary = 45000 },
            new Employee { Id = 3, Name = "Suresh Shah", Description = "manager", Salary = 60000 },
            new Employee { Id = 4, Name = "Anjali Bharadava", Description = "Developer", Salary = 55000 },
            new Employee { Id = 5, Name = "Nahii Jadav", Description = "Developer", Salary = 66000 },
            new Employee { Id = 6, Name = "Nishant Jain", Description = "Developer", Salary = 46000 }
        };

        #endregion

        #region API Methods

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        [HttpGet]
        [Route("api/employees")]
        public IHttpActionResult GetAllEmployees()
        {
            // Return the complete list of employees
            return Ok(employees);
        }

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>The employee matching the given id, or NotFound if no match is found.</returns>
        [HttpGet]
        [Route("api/employees/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            // Search for the employee by id
            var employee = employees.FirstOrDefault(e => e.Id == id);

            // Return 404 if the employee is not found
            if (employee == null) return NotFound();

            // Return the found employee
            return Ok(employee);
        }

        /// <summary>
        /// Retrieves employees by their description.
        /// </summary>
        /// <param name="description">The description of the employee.</param>
        /// <returns>A list of employees matching the given description, or NotFound if no matches are found.</returns>
        [HttpGet]
        [Route("api/employees/description/{description}")]
        public IHttpActionResult GetEmployeesByDescription(string description)
        {
            // Perform a case-insensitive search for all employees by description
            var matchingEmployees = employees.Where(e =>
                string.Equals(e.Description, description, StringComparison.OrdinalIgnoreCase)).ToList();

            // Return 404 if no matching employees are found
            if (!matchingEmployees.Any()) return NotFound();

            // Return the list of matching employees
            return Ok(matchingEmployees);
        }

        #endregion
    }
}
