using System;

namespace EmployeeManagementModel.Models
{
    /// <summary>
    /// Represents an Employee with basic details such as Id, Name, Email, Description, Salary, and Joining Date.
    /// </summary>
    public class EmployeeModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the employee.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the employee's role or position.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Gets or sets the joining date of the employee.
        /// </summary>
        public DateTime JoiningDate { get; set; }

        #endregion

        #region Constructor

        // Parameterless constructor
        public EmployeeModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeModel"/> class with specified values.
        /// </summary>
        /// <param name="id">The identifier for the employee.</param>
        /// <param name="name">The name of the employee.</param>
        /// <param name="email">The email of the employee.</param>
        /// <param name="description">A brief description of the employee's role.</param>
        /// <param name="salary">The salary of the employee.</param>
        /// <param name="joiningDate">The joining date of the employee.</param>
        public EmployeeModel(int id, string name, string email, string description, int salary, DateTime joiningDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Description = description;
            Salary = salary;
            JoiningDate = joiningDate;
        }

        #endregion
    }
}
