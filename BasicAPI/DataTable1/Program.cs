using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataTableExample
{
    public class Program
    {
        /// <summary>
        /// Demonstrates various operations with DataTable in C#.
        /// Includes creating, populating, sorting, filtering, and comparing DataTables.
        /// </summary>
        static void Main(string[] args)
        {
            /// <summary>
            /// Create and populate the employees DataTable.
            /// </summary>
            DataTable employees = new DataTable();

            // Define columns for the employees DataTable
            employees.Columns.Add("Id", typeof(int));
            employees.Columns.Add("Name", typeof(string));
            employees.Columns.Add("JobRole", typeof(string));
            employees.Columns.Add("Salary", typeof(decimal));

            // Variables for user input
            int employeeId;
            string employeeName, employeeJobRole;
            decimal employeeSalary;

            // Allow the user to input employee data dynamically
            while (true)
            {
                Console.Write("Enter 'exit' to quit or press Enter to add a new employee: ");
                string input = Console.ReadLine();

                // Exit condition
                if (input.ToLower() == "exit")
                    break;

                Console.Write("Employee ID: ");
                employeeId = int.Parse(Console.ReadLine());

                Console.Write("Employee Name: ");
                employeeName = Console.ReadLine();

                Console.Write("Employee JobRole: ");
                employeeJobRole = Console.ReadLine();

                Console.Write("Employee Salary: ");
                employeeSalary = decimal.Parse(Console.ReadLine());

                // Add a new row to employees DataTable
                DataRow row = employees.NewRow();
                row["Id"] = employeeId;
                row["Name"] = employeeName;
                row["JobRole"] = employeeJobRole;
                row["Salary"] = employeeSalary;
                employees.Rows.Add(row);
            }

            // Manually add additional employee rows
            AddEmployee(employees, 11, "Priyank", "Tester", 40000);
            AddEmployee(employees, 12, "Anjali", "Developer", 60000);
            AddEmployee(employees, 13, "Suresh", "Manager", 70000);
            AddEmployee(employees, 14, "Nahii", "HR", 55000);

            // Display all employees
            Console.WriteLine("All Employees:");
            DisplayDataTable(employees);

            /// <summary>
            /// Create and populate the person DataTable for comparison.
            /// </summary>
            DataTable persons = new DataTable();
            persons.Columns.Add("Id", typeof(int));
            persons.Columns.Add("Name", typeof(string));
            persons.Columns.Add("Occupation", typeof(string));
            persons.Columns.Add("Income", typeof(decimal));

            DataRow personRow = persons.NewRow();
            personRow["Id"] = 11;
            personRow["Name"] = "Priyank";
            personRow["Occupation"] = "Tester";
            personRow["Income"] = 40000;
            persons.Rows.Add(personRow);

            // Compare the two DataTables
            bool isSamePerson = employees.AsEnumerable().SequenceEqual(persons.AsEnumerable(), DataRowComparer.Default);
            Console.WriteLine($"Are the employees and persons tables the same? {isSamePerson}");

            // Select employees with a salary > 50000
            Console.WriteLine("Employees with a salary > 50000:");
            DataRow[] highSalaryEmployees = employees.Select("Salary > 50000");
            foreach (DataRow row in highSalaryEmployees)
            {
                Console.WriteLine(row["Name"]);
            }

            // Sort employees by salary in descending order
            Console.WriteLine("Employees sorted by salary in descending order:");
            employees.DefaultView.Sort = "Salary DESC";
            DataTable sortedEmployees = employees.DefaultView.ToTable();
            DisplayDataTable(sortedEmployees);

            // Filter employees with salary between 40000 and 50000
            Console.WriteLine("Employees with a salary between 40000 and 50000:");
            DataRow[] middleSalaryEmployees = employees.Select("Salary >= 40000 AND Salary <= 50000");
            foreach (DataRow row in middleSalaryEmployees)
            {
                Console.WriteLine(row["Name"]);
            }

            // Convert DataTable to a List<DataRow>
            List<DataRow> employeeList = employees.AsEnumerable().ToList();
            Console.WriteLine("Employees as a List:");
            foreach (DataRow row in employeeList)
            {
                Console.WriteLine($"{row["Id"]}, {row["Name"]}, {row["JobRole"]}, {row["Salary"]}");
            }

            // Convert List<DataRow> back to a new DataTable
            DataTable employeeTableFromList = CreateDataTableFromList(employeeList, employees.Columns);
            Console.WriteLine("DataTable reconstructed from List<DataRow>:");
            DisplayDataTable(employeeTableFromList);
        }

        /// <summary>
        /// Adds a row to a DataTable.
        /// </summary>
        /// <param name="table">The DataTable to which the row will be added.</param>
        /// <param name="id">The ID of the employee.</param>
        /// <param name="name">The name of the employee.</param>
        /// <param name="jobRole">The job role of the employee.</param>
        /// <param name="salary">The salary of the employee.</param>
        private static void AddEmployee(DataTable table, int id, string name, string jobRole, decimal salary)
        {
            DataRow row = table.NewRow();
            row["Id"] = id;
            row["Name"] = name;
            row["JobRole"] = jobRole;
            row["Salary"] = salary;
            table.Rows.Add(row);
        }

        /// <summary>
        /// Displays the contents of a DataTable.
        /// </summary>
        /// <param name="table">The DataTable to display.</param>
        private static void DisplayDataTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row["Id"]}, {row["Name"]}, {row["JobRole"]}, {row["Salary"]}");
            }
        }

        /// <summary>
        /// Creates a new DataTable from a List<DataRow>.
        /// </summary>
        /// <param name="rows">The list of DataRow objects.</param>
        /// <param name="columns">The column schema to apply to the new DataTable.</param>
        /// <returns>A new DataTable populated with the provided rows.</returns>
        private static DataTable CreateDataTableFromList(List<DataRow> rows, DataColumnCollection columns)
        {
            DataTable table = new DataTable();
            foreach (DataColumn column in columns)
            {
                table.Columns.Add(column.ColumnName, column.DataType);
            }

            foreach (DataRow row in rows)
            {
                DataRow newRow = table.NewRow();
                foreach (DataColumn column in columns)
                {
                    newRow[column.ColumnName] = row[column.ColumnName];
                }
                table.Rows.Add(newRow);
            }

            return table;
        }
    }
}
