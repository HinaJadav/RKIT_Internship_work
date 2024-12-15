using System.Text.Json;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    /// <summary>
    /// Handles operations related to employees management, such as adding, updating, deleting, and retrieving employee data.
    /// The data is stored and managed using a JSON file.
    /// </summary>
    public class EmployeeController
    {
        #region Private Properties
        
        /// <summary>
        /// The file path where employee data is stored.
        /// </summary>
        private const string FilePath = "employees.json";

        /// <summary>
        /// A list to hold all employee records loaded from the JSON file.
        /// </summary>
        private List<EmployeeModel> _employees;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class and loads employee data.
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
            var json = JsonSerializer.Serialize(_employees, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Loads the employee data from the JSON file.
        /// </summary>
        /// <returns>A list of employees; return an empty list if the file does not exist.</returns>
        private List<EmployeeModel> LoadEmployees()
        {
            // Check if the file exists before attempting to read
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);

                // Deserialize the JSON content to a list of employees
                return JsonSerializer.Deserialize<List<EmployeeModel>>(json) ?? new List<EmployeeModel>();
            }
            return new List<EmployeeModel>(); // Return an empty list if file does not exist
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the list of all employees.
        /// </summary>
        /// <returns>A list of <see cref="EmployeeModel"/> objects.</returns>
        public List<EmployeeModel> GetAllEmployee() => _employees;

        /// <summary>
        /// Adds a new employee to the system.
        /// </summary>
        /// <param name="employee">The employee model to be added.</param>
        public void AddEmployee(EmployeeModel employee)
        {
            employee.Id = _employees.Count + 1; // Auto incremented employee ID
            _employees.Add(employee); // Add to the list.
            SaveEmployees(); // Persist changes to the JSON file
        }

        /// <summary>
        /// Updates an existing employee's information.
        /// </summary>
        /// <param name="id">The Id of the employee to update.</param>
        /// <param name="updatedEmployee">The updated employee details.</param>
        /// <returns>True if update is successful; otherwise, false.</returns>
        public bool UpdateEmployee(int id, EmployeeModel updatedEmployee)
        {
            // Find the employee by Id
            EmployeeModel employee = _employees.FirstOrDefault(e => e.Id == id); 

            if (employee == null)
            {
                // If employee not found, return false
                return false;
            }

            // Update the employee properties
            // Retain old values if input is null
            employee.Name = updatedEmployee.Name ?? employee.Name;
            employee.Email = updatedEmployee.Email ?? employee.Email;
            employee.Description = updatedEmployee.Description ?? employee.Description;
            employee.Salary = updatedEmployee.Salary; 
            
            SaveEmployees(); // Persist changes to the JSON file
            return true;
        }

        /// <summary>
        /// Deletes an employee by their Id.
        /// </summary>
        /// <param name="id">The Id of the employee to delete.</param>
        /// <returns>True if the employee is successfully deleted; otherwise, false.</returns>
        public bool DeleteEmployee(int id)
        {
            EmployeeModel employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return false;

            // Remove the employee from the list
            _employees.Remove(employee);
            SaveEmployees();
            return true;
        }

        #endregion
    }
}
