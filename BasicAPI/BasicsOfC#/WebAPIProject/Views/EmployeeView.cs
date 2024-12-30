using WebAPIProject.Models;
using WebAPIProject.Controllers;

namespace WebAPIProject.Views
{
    /// <summary>
    /// Provides a console based user interface for the Employee Management System.
    /// Allows users to perform CRUD operations on employee records.
    /// </summary>
    public class EmployeeView
    {
        #region Private Properties

        /// <summary>
        /// Controller to handle employee operations.
        /// </summary>
        private EmployeeController _controller;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeView"/>  class.
        /// </summary>
        public EmployeeView()
        {
            _controller = new EmployeeController(); 
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the main menu for the employee Management System.
        /// </summary>
        public void Run()
        {
            bool running = true;

            while(running)
            {
                Console.WriteLine("\nEmployee Management System");
                Console.WriteLine("1. View All Employees");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Enter your choice: ");

                int choice = int.Parse(Console.ReadLine()); 

                switch(choice)
                {
                    case 1:
                        ViewAllEmployees();
                        break;
                    case 2:
                        AddEmployees();
                        break;
                    case 3:
                        UpdateEmployees();
                        break;
                    case 4:
                        DeleteEmployees();
                        break;
                    case 5:
                        running = false;
                        Console.WriteLine("---END---");
                        break;
                    default:
                        Console.WriteLine("Invalid choice please try again.");
                        break;

                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Views all employees in system.
        /// </summary>
        private void ViewAllEmployees()
        {
            List<EmployeeModel> employees = _controller.GetAllEmployee();

            Console.WriteLine("\n---EMPLOYEE LIST---");

            foreach(EmployeeModel employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}   Name: {employee.Name}   Email: {employee.Email} Description: {employee.Description} Salary: {employee.Salary}");
            }
        }

        /// <summary>
        /// Adds a new employee to the system.
        /// </summary>
        private void AddEmployees()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Enter salary: ");
            int salary = int.Parse(Console.ReadLine());

            EmployeeModel employee = new EmployeeModel(0, name, email, description, salary);

            _controller.AddEmployee(employee);

            Console.WriteLine("Employee added successfully.");
        }

        /// <summary>
        /// Updates an existing employee's details.
        /// If the user presses Enter without entering any value, the current value is retained.
        /// </summary>
        private void UpdateEmployees()
        {
            Console.Write("Enter employee Id to update: ");
            int id = int.Parse(Console.ReadLine());

            var existingEmployee = _controller.GetAllEmployee().FirstOrDefault(e => e.Id == id);

            if (existingEmployee == null)
            {
                Console.WriteLine("Employee Not Found.");
                return;
            }

            Console.Write($"Enter name ({existingEmployee.Name}): ");
            string name = Console.ReadLine();
            name = string.IsNullOrWhiteSpace(name) ? existingEmployee.Name : name; // Take old value if Enter is pressed

            Console.Write($"Enter email ({existingEmployee.Email}): ");
            string email = Console.ReadLine();
            email = string.IsNullOrWhiteSpace(email) ? existingEmployee.Email : email;

            Console.Write($"Enter description ({existingEmployee.Description}): ");
            string description = Console.ReadLine();
            description = string.IsNullOrWhiteSpace(description) ? existingEmployee.Description : description;

            Console.Write($"Enter salary ({existingEmployee.Salary}): ");
            string salaryInput = Console.ReadLine();
            int salary = string.IsNullOrWhiteSpace(salaryInput) ? existingEmployee.Salary : int.Parse(salaryInput);

            EmployeeModel updatedEmployee = new EmployeeModel(id, name, email, description, salary);

            bool isUpdated = _controller.UpdateEmployee(id, updatedEmployee);

            Console.WriteLine(isUpdated ? "Employee Updated Successfully." : "Error: Employee Not Updated.");
        }

        /// <summary>
        /// Deletes an employee based on the provided Id.
        /// </summary>
        private void DeleteEmployees()
        {
            Console.WriteLine("Enter employee Id to delete: ");
            int id = int.Parse(Console.ReadLine());

            bool isDeleted = _controller.DeleteEmployee(id);
            Console.WriteLine(isDeleted ? "Employee is deleted successfully." : "Employee not found.");
        }

        #endregion
    }
}
