using System;
using System.Data;
using EmployeeManagementController.Controllers;

namespace EmployeeManagementView.Views
{
    /// <summary>
    /// Provides a console-based user interface for the Employee Management System.
    /// Allows users to perform CRUD operations on employee records.
    /// </summary>
    public class EmployeeView
    {
        #region Private Fields 

        /// <summary>
        /// Controller to handle employee operations.
        /// </summary>
        private EmployeeController _controller;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeView"/> class.
        /// </summary>
        public EmployeeView()
        {
            _controller = new EmployeeController();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the main menu for the Employee Management System.
        /// </summary>
        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nEmployee Management System");
                Console.WriteLine("1. View All Employees");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ViewAllEmployees();
                        break;
                    case 2:
                        AddEmployee();
                        break;
                    case 3:
                        UpdateEmployee();
                        break;
                    case 4:
                        DeleteEmployee();
                        break;
                    case 5:
                        running = false;
                        Console.WriteLine("---END---");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Displays all employees in the system.
        /// </summary>
        private void ViewAllEmployees()
        {
            DataTable employees = _controller.GetAllEmployees();

            Console.WriteLine("\n---EMPLOYEE LIST---");

            foreach (DataRow row in employees.Rows)
            {
                Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Email: {row["Email"]}, Description: {row["Description"]}, Salary: {row["Salary"]}, Joining Date: {row["JoiningDate"]}");
            }
        }

        /// <summary>
        /// Adds a new employee to the system.
        /// </summary>
        private void AddEmployee()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Enter salary: ");
            if (!int.TryParse(Console.ReadLine(), out int salary))
            {
                Console.WriteLine("Invalid input. Salary must be a number.");
                return;
            }

            Console.Write("Enter joining date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime joiningDate))
            {
                Console.WriteLine("Invalid input. Enter a valid date.");
                return;
            }

            _controller.AddEmployee(name, email, description, salary, joiningDate);
            Console.WriteLine("Employee added successfully.");
        }

        /// <summary>
        /// Updates an existing employee's details.
        /// </summary>
        private void UpdateEmployee()
        {
            Console.Write("Enter the ID of the employee you want to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
                return;
            }

            Console.Write("Enter new Name (or press Enter to keep existing): ");
            string name = Console.ReadLine();

            Console.Write("Enter new Email (or press Enter to keep existing): ");
            string email = Console.ReadLine();

            Console.Write("Enter new Description (or press Enter to keep existing): ");
            string description = Console.ReadLine();

            Console.Write("Enter new Salary (or press Enter to keep existing): ");
            string salaryInput = Console.ReadLine();
            int salary = 0; 
            if (!string.IsNullOrWhiteSpace(salaryInput) && int.TryParse(salaryInput, out int parsedSalary))
            {
                salary = parsedSalary;
            }

            Console.Write("Enter new Joining Date (yyyy-MM-dd, or press Enter to keep existing): ");
            string dateInput = Console.ReadLine();
            DateTime joiningDate = new DateTime();
            if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime parsedDate))
            {
                joiningDate = parsedDate;
            }

            bool isUpdated = _controller.UpdateEmployee(id, name, email, description, salary, joiningDate);
            Console.WriteLine(isUpdated ? "Employee updated successfully." : "Employee not found.");
        }

        /// <summary>
        /// Deletes an employee based on the provided ID.
        /// </summary>
        private void DeleteEmployee()
        {
            Console.Write("Enter the employee ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
                return;
            }

            bool isDeleted = _controller.DeleteEmployee(id);
            Console.WriteLine(isDeleted ? "Employee deleted successfully." : "Employee not found.");
        }

        #endregion
    }
}
