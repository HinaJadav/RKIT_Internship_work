using System.Collections.Generic;
using System.IO;
using System.Linq;
using EmployeeManagementModel.Models;
using Newtonsoft.Json; 

namespace EmployeeManagementController.Controllers
{
    /// <summary>
    /// Handles operations related to employee management, such as adding, updating,
    /// deleting, and retrieving employee data. The data is stored and managed using a JSON file.
    /// </summary>
    public class EmployeeController
    {
        #region Private Properties

        /// <summary>
        /// The file path where employee data is stored as JSON.
        /// </summary>
        private const string FilePath = "employees.json";

        /// <summary>
        /// A list to hold all employee records loaded from the JSON file.
        /// </summary>
        private List<EmployeeModel> _employees;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class
        /// and loads employee data from the JSON file.
        /// </summary>
        public EmployeeController()
        {
            _employees = LoadEmployees();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the current list of employees to the JSON file.
        /// </summary>
        private void SaveEmployees()
        {
            // Serialize the employee list into JSON format with indentation for readability
            var json = JsonConvert.SerializeObject(_employees, Formatting.Indented);        
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Loads the employee data from the JSON file.
        /// If the file does not exist or is empty, an empty list is returned.
        /// </summary>
        /// <returns>A list of <see cref="EmployeeModel"/> objects.</returns>
        private List<EmployeeModel> LoadEmployees()
        {
            // Check if the file exists before attempting to read
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                // Deserialize JSON content to a list of employees
                return JsonConvert.DeserializeObject<List<EmployeeModel>>(json) ?? new List<EmployeeModel>();
            }
            return new List<EmployeeModel>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the list of all employees.
        /// </summary>
        /// <returns>A list of <see cref="EmployeeModel"/> objects.</returns>
        public List<EmployeeModel> GetAllEmployee() => _employees;

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The <see cref="EmployeeModel"/> object if found; otherwise, null.</returns>
        public EmployeeModel GetEmployeeById(int id)
        {
            // Use LINQ to find the employee with the specified ID
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Adds a new employee to the system.
        /// The employee's ID is auto-incremented based on the list count.
        /// </summary>
        /// <param name="employee">The <see cref="EmployeeModel"/> object to be added.</param>
        public void AddEmployee(EmployeeModel employee)
        {
            // Auto-increment employee ID
            employee.Id = _employees.Count + 1;
            // Add the employee to the list
            _employees.Add(employee);
            // Save the updated list to the JSON file
            SaveEmployees();
        }

        /// <summary>
        /// Updates an existing employee's information.
        /// If fields are left null, the current values are retained.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="updatedEmployee">The updated <see cref="EmployeeModel"/> details.</param>
        /// <returns>True if the update is successful; otherwise, false.</returns>
        public bool UpdateEmployee(int id, EmployeeModel updatedEmployee)
        {
            // Find the employee by ID
            EmployeeModel employee = _employees.Find(e => e.Id == id);

            if (employee == null)
            {
                // Employee not found
                return false;
            }

            // Update fields, retain old values if null
            employee.Name = updatedEmployee.Name ?? employee.Name;
            employee.Email = updatedEmployee.Email ?? employee.Email;
            employee.Description = updatedEmployee.Description ?? employee.Description;
            employee.Salary = updatedEmployee.Salary;

            // Save the updated list to the JSON file
            SaveEmployees();
            return true;
        }

        /// <summary>
        /// Deletes an employee from the system based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>True if the employee is successfully deleted; otherwise, false.</returns>
        public bool DeleteEmployee(int id)
        {
            // Find the employee by ID
            var employee = _employees.Find(e => e.Id == id);
            if (employee == null)
            {
                // Employee not found
                return false;
            }

            // Remove the employee from the list
            _employees.Remove(employee);
            // Save the updated list to the JSON file
            SaveEmployees();
            return true;

        }

        #endregion
    }
}
