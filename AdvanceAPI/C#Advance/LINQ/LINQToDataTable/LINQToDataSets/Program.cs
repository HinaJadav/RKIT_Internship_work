using System;
using System.Data;
using System.Linq;

namespace LINQToDataSets
{
    public class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Initialize the Students DataTable with columns for student details.
            /// </summary>
            DataTable students = new DataTable();
            students.Columns.Add("Id", typeof(int));
            students.Columns.Add("Name", typeof(string));
            students.Columns.Add("CPI", typeof(double));
            students.Columns.Add("Fees", typeof(int));
            students.Columns.Add("Package", typeof(int));
            students.Columns.Add("isPlaced", typeof(bool));
            students.Columns.Add("Gender", typeof(string));
            students.Columns.Add("DepartmentId", typeof(int));

            /// <summary>
            /// Insert sample student records into the Students DataTable.
            /// </summary>
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

            /// <summary>
            /// Display student data for verification of records in the Students DataTable.
            /// </summary>
            Console.WriteLine("Students Data:");
            foreach (DataRow student in students.Rows)
            {
                Console.WriteLine($"Id: {student["Id"]}, Name: {student["Name"]}, DepartmentId: {student["DepartmentId"]}, CPI: {student["CPI"]}, Fees: {student["Fees"]}, Package: {student["Package"]}, Placed: {student["isPlaced"]}, Gender: {student["Gender"]}");
            }

            /// <summary>
            /// Initialize the Departments DataTable with columns for department details.
            /// </summary>
            DataTable departments = new DataTable();
            departments.Columns.Add("DepartmentId", typeof(int));
            departments.Columns.Add("DepartmentName", typeof(string));

            /// <summary>
            /// Insert sample department records into the Departments DataTable.
            /// </summary>
            departments.Rows.Add(1, "Computer Engineering");
            departments.Rows.Add(2, "Electronic Engineering");
            departments.Rows.Add(3, "Information & technology");

            /// <summary>
            /// Display department data for verification of records in the Departments DataTable.
            /// </summary>
            Console.WriteLine("\nDepartment Data:");
            foreach (DataRow department in departments.Rows)
            {
                Console.WriteLine($"DepartmentId: {department["DepartmentId"]}, DepartmentName: {department["DepartmentName"]}");
            }

            /// <summary>
            /// Group the students by their department name using GroupJoin().
            /// This shows the students belonging to each department.
            /// </summary>
            var departmentWiseStudentName = from dept in departments.AsEnumerable()
                                            join stut in students.AsEnumerable()
                                            on dept.Field<int>("DepartmentId") equals stut.Field<int>("DepartmentId") into studentGroup
                                            select new
                                            {
                                                DepartmentName = dept.Field<string>("DepartmentName"),
                                                StudentsList = studentGroup.Select(student => student.Field<string>("Name")).ToList()  // Collecting the list of student names
                                            };

            /// <summary>
            /// Display the list of students grouped by department name.
            /// </summary>
            Console.WriteLine("\nList of students name within each department:");
            foreach (var item in departmentWiseStudentName)
            {
                Console.WriteLine($"Department: {item.DepartmentName}");
                foreach (var studentName in item.StudentsList)
                {
                    Console.WriteLine($"  - {studentName}");
                }
            }

            /// <summary>
            /// Perform an inner join to list all students' names along with their department names.
            /// </summary>
            var studentWithDepartmentnames = from stut in students.AsEnumerable()
                                             join dept in departments.AsEnumerable()
                                             on stut.Field<int>("DepartmentId") equals dept.Field<int>("DepartmentId")
                                             select new
                                             {
                                                 StudentName = stut.Field<string>("Name"),
                                                 DepartmentName = dept.Field<string>("DepartmentName")
                                             };

            /// <summary>
            /// Display student names with their department names.
            /// </summary>
            Console.WriteLine("\nList of students name with their department names: ");
            foreach (var item in studentWithDepartmentnames)
            {
                Console.WriteLine($"Student: {item.StudentName}, Department: {item.DepartmentName}");
            }

            /// <summary>
            /// Filter placed students and display their Id, Name, and Package details.
            /// </summary>
            var placedStudentsView = from stut in students.AsEnumerable()
                                     where stut.Field<bool>("isPlaced") == true
                                     select new
                                     {
                                         Id = stut.Field<int>("Id"),
                                         Name = stut.Field<string>("Name"),
                                         Package = stut.Field<int>("Package")
                                     };

            /// <summary>
            /// Display placed students with basic information (Id, Name, Package).
            /// </summary>
            Console.WriteLine("\nView of placed students with basic information:");
            foreach (var student in placedStudentsView)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Package: {student.Package}");
            }

            /// <summary>
            /// Find placed students grouped by their gender using GroupBy().
            /// </summary>
            var placedStudentsGroupedByGender = from stut in students.AsEnumerable()
                                                where stut.Field<bool>("isPlaced") == true
                                                group stut by stut.Field<string>("Gender") into genderGroup
                                                select new
                                                {
                                                    Gender = genderGroup.Key,
                                                    Students = genderGroup.Select(s => new
                                                    {
                                                        Name = s.Field<string>("Name"),
                                                        Package = s.Field<int>("Package")
                                                    }).ToList()
                                                };

            /// <summary>
            /// Calculating the Average Package of Placed Students Per Department
            /// </summary>
            var averagePackagePerDepartment = students.AsEnumerable()
                .Where(stu => stu.Field<bool>("isPlaced")) // Consider only placed students
                .GroupBy(stu => stu.Field<int>("DepartmentId")) // Group by DepartmentId
                .Select(group => new
                {
                    DepartmentId = group.Key,
                    AveragePackage = group.Average(stu => stu.Field<int>("Package"))
                })
                .Join(departments.AsEnumerable(),
                      deptGroup => deptGroup.DepartmentId,
                      dept => dept.Field<int>("DepartmentId"),
                      (deptGroup, dept) => new
                      {
                          DepartmentName = dept.Field<string>("DepartmentName"),
                          AveragePackage = deptGroup.AveragePackage
                      });

            Console.WriteLine("\nAverage Package of Placed Students Per Department:");
            foreach (var item in averagePackagePerDepartment)
            {
                Console.WriteLine($"Department: {item.DepartmentName}, Average Package: {item.AveragePackage}");
            }


            Console.ReadKey();
        }
    }
}
