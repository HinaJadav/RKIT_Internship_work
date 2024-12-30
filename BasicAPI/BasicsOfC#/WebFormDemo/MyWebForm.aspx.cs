using System;
using System.Collections.Generic;

namespace WebFormDemo
{
    /// <summary>
    /// Code-behind class for the employee form that handles user interaction.
    /// </summary>
    public partial class MyWebForm : System.Web.UI.Page
    {
        #region Fields

        /// <summary>
        /// A static list to simulate a database for storing employee records.
        /// </summary>
        private static List<Employee> employeeList = new List<Employee>();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Saves a new employee to the list.
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee
            {
                FirstName = FName.Text,
                LastName = LName.Text,
                Description = Description.Text,
                Salary = Salary.Text
            };

            employeeList.Add(emp);
            BindGrid();
            ClearFields();
        }

        /// <summary>
        /// Resets the form fields.
        /// </summary>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        /// <summary>
        /// Handles row deletion from the GridView.
        /// </summary>
        protected void EmployeeGrid_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            employeeList.RemoveAt(e.RowIndex);
            BindGrid();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Binds the employee list to the GridView for display.
        /// </summary>
        private void BindGrid()
        {
            EmployeeGrid.DataSource = employeeList;
            EmployeeGrid.DataBind();
        }

        /// <summary>
        /// Clears all input fields on the form.
        /// </summary>
        private void ClearFields()
        {
            FName.Text = string.Empty;
            LName.Text = string.Empty;
            Description.Text = string.Empty;
            Salary.Text = string.Empty;
        }

        #endregion
    }

    #region Employee Class

    /// <summary>
    /// Represents an employee with basic information.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the employee.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        public string Salary { get; set; }
    }

    #endregion
}
