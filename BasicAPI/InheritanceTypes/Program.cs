using System;

namespace OOPInheritance
{
    /// <summary>
    /// Base class representing a generic employee.
    /// Demonstrates Single Inheritance.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        public int EmployeeId { get; set; }

        public Employee(string name, int employeeId)
        {
            Name = name;
            EmployeeId = employeeId;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Employee Name: {Name}, ID: {EmployeeId}");
        }
    }

    /// <summary>
    /// Derived class representing a Manager.
    /// Demonstrates Hierarchical Inheritance.
    /// </summary>
    public class Manager : Employee
    {
        /// <summary>
        /// Gets or sets the department of the manager.
        /// </summary>
        public string Department { get; set; }

        public Manager(string name, int employeeId, string department)
            : base(name, employeeId)
        {
            Department = department;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Department: {Department}");
        }
    }

    /// <summary>
    /// Derived class representing a Developer.
    /// Demonstrates Multilevel Inheritance.
    /// </summary>
    public class Developer : Employee
    {
        /// <summary>
        /// Gets or sets the programming language used by the developer.
        /// </summary>
        public string ProgrammingLanguage { get; set; }

        public Developer(string name, int employeeId, string programmingLanguage)
            : base(name, employeeId)
        {
            ProgrammingLanguage = programmingLanguage;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Programming Language: {ProgrammingLanguage}");
        }
    }

    /// <summary>
    /// Derived class representing a Tester.
    /// Demonstrates Multiple Inheritance using Interfaces.
    /// </summary>
    public interface ITestable
    {
        void PerformTesting();
    }

    public class Tester : Employee, ITestable
    {
        /// <summary>
        /// Gets or sets the testing tool used by the tester.
        /// </summary>
        public string TestingTool { get; set; }

        public Tester(string name, int employeeId, string testingTool)
            : base(name, employeeId)
        {
            TestingTool = testingTool;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Testing Tool: {TestingTool}");
        }

        public void PerformTesting()
        {
            Console.WriteLine($"Tester {Name} is performing tests using {TestingTool}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Single Inheritance: Employee
            Employee emp = new Employee("Priyank Jadav", 1001);
            emp.DisplayInfo();

            Console.WriteLine();

            // Hierarchical Inheritance: Manager
            Manager manager = new Manager("Anjali Jadav", 1002, "HR");
            manager.DisplayInfo();

            Console.WriteLine();

            // Multilevel Inheritance: Developer
            Developer developer = new Developer("Suresh Jadav", 1003, "C#");
            developer.DisplayInfo();

            Console.WriteLine();

            // Multiple Inheritance via Interface: Tester
            Tester tester = new Tester("Madhu Jadav", 1004, "Selenium");
            tester.DisplayInfo();
            tester.PerformTesting();

            Console.ReadLine();
        }
    }
}
