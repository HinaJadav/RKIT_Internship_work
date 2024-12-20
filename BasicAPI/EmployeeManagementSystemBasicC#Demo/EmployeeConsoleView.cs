using System;
using System.Collections.Generic;
using EmployeeManagementModel.Models;
using EmployeeManagementController.Controllers;

namespace EmployeeManagementView.Views
{
    /// <summary>
    /// Provides a console-based user interface for the Employee Management System.
    /// Allows users to perform CRUD operations on employee records.
    /// </summary>
    public class EmployeeView
    {
        #region Private Feilds 

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
                Console.WriteLine("Enter your choice: ");

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
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Views all employees in the system.
        /// </summary>
        private void ViewAllEmployees()
        {
            List<EmployeeModel> employees = _controller.GetAllEmployee();

            Console.WriteLine("\n---EMPLOYEE LIST---");

            foreach (EmployeeModel employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}   Name: {employee.Name}   Email: {employee.Email}   Description: {employee.Description}   Salary: {employee.Salary}");
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
            if (!int.TryParse(Console.ReadLine(), out int salary))
            {
                Console.WriteLine("Invalid input. Salary must be a number.");
                return;
            }

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
            Console.Write("Enter the ID of the employee you want to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
                return;
            }

            // Call the GetEmployeeById method to retrieve the existing employee
            var employee = _controller.GetEmployeeById(id);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            // Display current details and prompt for updates
            Console.WriteLine($"Current Name: {employee.Name}");
            Console.Write("Enter new Name (or press Enter to keep it): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                employee.Name = newName;
            }

            Console.WriteLine($"Current Email: {employee.Email}");
            Console.Write("Enter new Email (or press Enter to keep it): ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                employee.Email = newEmail;
            }

            Console.WriteLine($"Current Description: {employee.Description}");
            Console.Write("Enter new Description (or press Enter to keep it): ");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                employee.Description = newDescription;
            }

            Console.WriteLine($"Current Salary: {employee.Salary}");
            Console.Write("Enter new Salary (or press Enter to keep it): ");
            string newSalaryInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSalaryInput) && int.TryParse(newSalaryInput, out int newSalary))
            {
                employee.Salary = newSalary;
            }

            bool isUpdated = _controller.UpdateEmployee(id, employee);
            Console.WriteLine(isUpdated ? "Employee updated successfully." : "Employee is not found.");
        }

        /// <summary>
        /// Deletes an employee based on the provided ID.
        /// </summary>
        private void DeleteEmployees()
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
