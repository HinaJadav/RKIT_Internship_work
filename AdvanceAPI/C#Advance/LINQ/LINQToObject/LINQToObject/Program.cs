using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LINQToObject
{
    public class Program
    {
        static void Main(string[] args)
        {
            // List to Object

            List<Student> students = new List<Student>
            {
                new Student(1, "Aarav Sharma", 8.5, 120000, 600000, true, "Male", 1),
                new Student(2, "Ishita Verma", 9.2, 110000, 750000, true, "Female", 2),
                new Student(3, "Rohan Singh", 7.8, 115000, 550000, true, "Male", 3),
                new Student(4, "Ananya Gupta", 8.1, 118000, 0, false, "Female", 1),
                new Student(5, "Vivaan Patel", 9.0, 112000, 800000, true, "Male", 2),
                new Student(6, "Sanya Joshi", 7.5, 125000, 0, false, "Female", 3),
                new Student(7, "Kabir Kumar", 8.8, 119000, 720000, true, "Male", 1),
                new Student(8, "Priya Mehta", 8.3, 121000, 0, false, "Female", 2),
                new Student(9, "Aditya Roy", 9.1, 116000, 780000, true, "Male", 3),
                new Student(10, "Riya Jain", 10.0, 122000, 0, false, "Female", 1)
            };

            // print all student information 
            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, CPI: {student.CPI}, " +
                                  $"Fees: {student.Fees}, Package: {student.Package}, Placed: {student.isPlace}, " +
                                  $"Gender: {student.Gender}, DepartmentID: {student.DepartmentID}");
            }


            // Queries using LINQ 

            // Aggregates:

            // Sum()
            // 1) Find total amount of Fees that College collect from all students
            int totalFees = students.Sum(student => student.Fees);
            Console.WriteLine($"\nTotal Fees Collected: {totalFees} Rs.");

            // Average()
            // 2) Find average package amount of college
            double averagePackage = students.Average(student => student.Package);
            Console.WriteLine($"\nAverage package of College: {averagePackage} Rs.");

            // Count()
            // 3) Count how many students are placed
            int noOfStudentPlaced = students.Count(student => student.isPlace == true);
            Console.WriteLine($"\nNumber of student placed: {noOfStudentPlaced}");

            // Max()
            // Method Syntax of LINQ
            // 4) Find maximum packaged student name
            string maximumPackageStudentName = students
                .Where(student => student.Package == students.Max(s => s.Package))
                .Select(student => student.Name).FirstOrDefault();
            Console.WriteLine($"\nStudent with the maximum package: {maximumPackageStudentName}");

            // Min()
            // Query Syntax of LINQ
            // 5) Find minimum packaged student name
            string minimumPackageStudentName = (from student in students where student.Package == (from s in students select s.Package).Min() select student.Name).FirstOrDefault();
            Console.WriteLine($"\nStudent with the minimum package: {minimumPackageStudentName}");

            // All()
            // 6) Check that all students are placed or not?
            bool isAllPlaced = students.All(student => student.isPlace == true);
            Console.WriteLine($"\nCheck that all college students are placed or not: {isAllPlaced}");
           

            // Any()
            // 7) Check that any student have 10 CPI.
            bool isAnyStudentHasFullCPI = (from student in students
                                           where student.CPI == 10.0
                                           select student).Any();
            Console.WriteLine($"\nCheck that any college students have 10.0 CPI: {isAnyStudentHasFullCPI}");
          
            // ToDictionary()
            // 8) Create dictionary of students with their student Id from students list
            Dictionary<int, string> studentsDictionary = students.ToDictionary(student => student.Id, student => student.Name);
            Console.WriteLine($"\nAll students name with their Ids:");
            foreach (var student in studentsDictionary)
            {
                Console.WriteLine($"ID: {student.Key}, Name: {student.Value}");
            }

            // print dictionary

            // toLookup()
            // 9) Generate lookup data based on student departmentId and name
            ILookup<int, string> studentsLookup = students.ToLookup(student => student.DepartmentID, student => student.Name);

            Console.WriteLine($"\n Generate Lookup table: ");
            // print lookup table
            foreach (var department in studentsLookup)
            {
                Console.WriteLine($"Department ID: {department.Key}");
                foreach (var name in department)
                {
                    Console.WriteLine($"    Name: {name}");
                }
            }
            

            Console.ReadKey();
        }
    }
}
