using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LINQToDataSets
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Students dataTable
            DataTable students = new DataTable();
            students.Columns.Add("Id", typeof(int));
            students.Columns.Add("Name", typeof(string));
            students.Columns.Add("CPI", typeof(double));
            students.Columns.Add("Fees", typeof(int));
            students.Columns.Add("Package", typeof(int));
            students.Columns.Add("isPlaced", typeof(bool));
            students.Columns.Add("Gender", typeof(string));
            students.Columns.Add("DepartmentId", typeof(int));

            // Insert data into students dataTable
            students.Rows.Add(1, "Aarav Sharma", 8.5, 120000, 600000, true, "Male", 1);
            students.Rows.Add(2, "Ishita Verma", 9.2, 110000, 750000, true, "Female", 2);
            students.Rows.Add(3, "Rohan Singh", 7.8, 115000, 550000, true, "Male", 3);
            students.Rows.Add(4, "Ananya Gupta", 8.1, 118000, 0, false, "Female", 1);
            students.Rows.Add(5, "Vivaan Patel", 9.0, 112000, 800000, true, "Male", 2);
            students.Rows.Add(6, "Sanya Joshi", 7.5, 125000, 0, false, "Female", 3);
            students.Rows.Add(7, "Kabir Kumar", 8.8, 119000, 720000, true, "Male", 1);
            students.Rows.Add(8, "Priya Mehta", 8.3, 121000, 0, false, "Female", 2);
            students.Rows.Add(9, "Aditya Roy", 9.1, 116000, 780000, true, "Male", 3);
            students.Rows.Add(10, "Riya Jain", 10.0, 122000, 0, false, "Female", 1);

            // Printing the student data from the DataTable
            Console.WriteLine("Students Data:");
            foreach (DataRow student in students.Rows)
            {
                Console.WriteLine($"Id: {student["Id"]}, Name: {student["Name"]}, DepartmentId: {student["DepartmentId"]}, CPI: {student["CPI"]}, Fees: {student["Fees"]}, Package: {student["Package"]}, Placed: {student["isPlaced"]}, Gender: {student["Gender"]}");
            }

            // Department dataTable
            DataTable departments = new DataTable();
            departments.Columns.Add("DepartmentId", typeof(int));
            departments.Columns.Add("DepartmentName", typeof(string));

            // Insert data into department dataTable
            departments.Rows.Add(1, "Computer Engineering");
            departments.Rows.Add(2, "Electronic Engineering");
            departments.Rows.Add(3, "Information & technology");

            // print entire dataTable data
            Console.WriteLine("\nDepartment Data:");
            foreach (DataRow department in departments.Rows)
            {
                Console.WriteLine($"DepartmentId: {department["DepartmentId"]}, DepartmentName: {department["DepartmentName"]}");
            }

            // Joins
            // GroupJoin()
            // 1) Find list of students name within each department 
            var departmentWiseStudentName = from dept in departments.AsEnumerable()
                                            join stut in students.AsEnumerable()
                                            on dept.Field<int>("DepartmentId") equals stut.Field<int>("DepartmentId") into studentGroup
                                            select new
                                            {
                                                DepartmentName = dept.Field<string>("DepartmentName"),
                                                StudentsList = studentGroup.Select(student => student.Field<string>("Name")).ToList()  // Collecting the list of student names
                                            };

            // Display results
            Console.WriteLine("\nList of students name within each department:");
            foreach (var item in departmentWiseStudentName)
            {
                Console.WriteLine($"Department: {item.DepartmentName}");
                foreach (var studentName in item.StudentsList)
                {
                    Console.WriteLine($"  - {studentName}");
                }
            }

            // AsEnumerable(): This method converts DataTable to an IEnumerable<DataRow>, allowing LINQ to operate on DataTable


            // InnerJoin()
            // 2) Print all students' names with their department names
            var studentWithDepartmentnames = from stut in students.AsEnumerable()
                                             join dept in departments.AsEnumerable()
                                             on stut.Field<int>("DepartmentId") equals dept.Field<int>("DepartmentId")
                                             select new
                                             {
                                                 StudentName = stut.Field<string>("Name"), // Access Name field using Field<>
                                                 DepartmentName = dept.Field<string>("DepartmentName") // Access DepartmentName field using Field<>
                                             };

            // Display the results
            Console.WriteLine("\nList of students name with their department names: ");
            foreach (var item in studentWithDepartmentnames)
            {
                Console.WriteLine($"Student: {item.StudentName}, Department: {item.DepartmentName}");
            }

            // select()
            // Find placed students
            var placedStudentsView = from stut in students.AsEnumerable()
                                     where stut.Field<bool>("isPlaced") == true  
                                     select new
                                     {
                                         Id = stut.Field<int>("Id"),
                                         Name = stut.Field<string>("Name"),
                                         Package = stut.Field<int>("Package")
                                     };

            // Display the results
            Console.WriteLine("\nView of placed students with basic information:");
            foreach (var student in placedStudentsView)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Package: {student.Package}");
            }


            Console.ReadKey();
        }
    }
}
