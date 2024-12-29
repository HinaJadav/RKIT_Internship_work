using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace EmployeeManagementController.Controllers
{
    public class EmployeeController
    {
        #region Private Properties

        private const string FilePath = "employees.json";

        private DataTable _employeeTable;

        #endregion

        #region Constructor

        public EmployeeController()
        {
            _employeeTable = CreateEmployeeTable();
            LoadEmployees();
        }

        #endregion

        #region Private Methods

        private DataTable CreateEmployeeTable()
        {
            DataTable table = new DataTable("Employees");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Salary", typeof(int));
            table.Columns.Add("JoiningDate", typeof(DateTime));
            return table;
        }

        private void SaveEmployees()
        {
            var employees = _employeeTable.AsEnumerable().Select(row => new
            {
                Id = row.Field<int>("Id"),
                Name = row.Field<string>("Name"),
                Email = row.Field<string>("Email"),
                Description = row.Field<string>("Description"),
                Salary = row.Field<int>("Salary"),
                JoiningDate = row.Field<DateTime>("JoiningDate").ToString("yyyy-MM-dd")
            }).ToList();

            string json = JsonConvert.SerializeObject(employees, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        private void LoadEmployees()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var employees = JsonConvert.DeserializeObject<List<dynamic>>(json);

                if (employees != null)
                {
                    foreach (var emp in employees)
                    {
                        _employeeTable.Rows.Add(
                            (int)emp.Id,
                            (string)emp.Name,
                            (string)emp.Email,
                            (string)emp.Description,
                            (int)emp.Salary,
                            DateTime.TryParse((string)emp.JoiningDate, out DateTime joiningDate) ? joiningDate : DateTime.MinValue
                        );
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        public DataTable GetAllEmployees() => _employeeTable;

        public DataRow GetEmployeeById(int id)
        {
            return _employeeTable.AsEnumerable().FirstOrDefault(row => row.Field<int>("Id") == id);
        }

        public void AddEmployee(string name, string email, string description, int salary, DateTime joiningDate)
        {
            int newId = _employeeTable.Rows.Count > 0
                ? _employeeTable.AsEnumerable().Max(row => row.Field<int>("Id")) + 1
                : 1;

            _employeeTable.Rows.Add(newId, name, email, description, salary, joiningDate);
            SaveEmployees();
        }

        public bool UpdateEmployee(int id, string name, string email, string description, int salary, DateTime joiningDate)
        {
            DataRow employee = GetEmployeeById(id);

            if (employee == null)
                return false;

            employee["Name"] = name;
            employee["Email"] = email;
            employee["Description"] = description;
            employee["Salary"] = salary;
            employee["JoiningDate"] = joiningDate;

            SaveEmployees();
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            DataRow employee = GetEmployeeById(id);

            if (employee == null)
                return false;

            _employeeTable.Rows.Remove(employee);
            SaveEmployees();
            return true;
        }

        #endregion
    }
}
