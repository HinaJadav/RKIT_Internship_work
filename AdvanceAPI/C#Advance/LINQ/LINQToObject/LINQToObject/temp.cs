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
                                  $"Fees: {student.Fees}, Package: {student.Package}, Placed: {student.isPlaced}, " +
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
            int noOfStudentPlaced = students.Count(student => student.isPlaced == true);
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
            bool isAllPlaced = students.All(student => student.isPlaced == true);
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

            Console.WriteLine($"\nGenerate Lookup table: ");
            // print lookup table
            foreach (var department in studentsLookup)
            {
                Console.WriteLine($"Department ID: {department.Key}");
                foreach (string name in department)
                {
                    Console.WriteLine($"    Name: {name}");
                }
            }


            // OrderByDescending()
            // ThenBy()
            // 10) To create an array of student names ordered in descending order by their package and then by their name

            string[] orderedPlacedStudentNames = students
            .Where(student => student.isPlaced) // Filter for placed students
            .OrderByDescending(student => student.Package)
            .ThenBy(student => student.Name)
            .Select(student => student.Name)
            .ToArray();

            Console.WriteLine("\nStudents ordered by descending package and then by name:");
            foreach (string name in orderedPlacedStudentNames)
            {
                Console.WriteLine(name);
            }


            // LINQ to Array

            // ElementAt()
            // 11) Find the student who has the 3rd highest package
            string thirdHighestPackageStudent = orderedPlacedStudentNames.ElementAt(2);
            Console.WriteLine($"\nStudent with the 3rd highest package: {thirdHighestPackageStudent}");

            // First()
            // 12) Find the student with the highest package
            string highestPackageStudent = orderedPlacedStudentNames.First();
            Console.WriteLine($"\nStudent with the highest package: {highestPackageStudent}");

            // FirstOrDefault()
            // 13) Find the student with the highest package or default if none
            string highestPackageStudentOrDefault = orderedPlacedStudentNames.FirstOrDefault();
            Console.WriteLine($"\nStudent with the highest package (or default): {highestPackageStudentOrDefault}");

            // Last()
            // 14) Find the student with the lowest package
            string lowestPackageStudent = orderedPlacedStudentNames.Last();
            Console.WriteLine($"\nStudent with the lowest package: {lowestPackageStudent}");

            // LastOrDefault()
            // 15) Find the student with the lowest package or default if none
            string lowestPackageStudentOrDefault = orderedPlacedStudentNames.LastOrDefault();
            Console.WriteLine($"\nStudent with the lowest package (or default): {lowestPackageStudentOrDefault}");

            // Skip()
            // 16) Get the list of students excluding the top 3 by package
            string[] skippedPlacedStudents = orderedPlacedStudentNames.Skip(3).ToArray();
            Console.WriteLine("\nStudents excluding the top 3 by package:");
            foreach (string student in skippedPlacedStudents)
            {
                Console.WriteLine(student);
            }

            // Take()
            // 17) Get the list of the top 3 students by package
            string[] topThreePlacedStudents = orderedPlacedStudentNames.Take(3).ToArray();
            Console.WriteLine("\nTop 3 students by package:");
            foreach (string student in topThreePlacedStudents)
            {
                Console.WriteLine(student);
            }

            // Contains()
            // 18) Check if "Vivaan" is in the list of placed students
            bool isVivaanPlaced = orderedPlacedStudentNames.Contains("Vivaan");
            Console.WriteLine($"Is Vivaan placed? {isVivaanPlaced}");

            // Concat()
            // 19) Add "Sanya Joshi" to the list of placed students
            string[] updatedPlacedStudentNames = orderedPlacedStudentNames.Concat(new[] { "Sanya Joshi" }).ToArray();
            Console.WriteLine("\nUpdated list of placed students (including Sanya Joshi):");
            foreach (string student in updatedPlacedStudentNames)
            {
                Console.WriteLine(student);
            }

            // where()
            // 20) Find students with a package between 400,000 and 600,000
            string[] packageRangeStudentNames = students
                .Where(student => student.isPlaced && student.Package >= 400000 && student.Package <= 600000)
                .Select(student => student.Name)
                .ToArray();

            Console.WriteLine("\nStudents with a package between 400,000 and 600,000:");
            foreach (string name in packageRangeStudentNames)
            {
                Console.WriteLine(name);
            }

            // GroupBy()
            // 21) ind all Male and Female
            var studentsByGender = students
                .GroupBy(student => student.Gender)
                .Select(group => new
                {
                    Gender = group.Key,
                    Names = group.Select(student => student.Name).ToList()
                });

            Console.WriteLine("\nStudents grouped by gender:");
            foreach (var group in studentsByGender)
            {
                Console.WriteLine($"Gender: {group.Gender}");
                foreach (var name in group.Names)
                {
                    Console.WriteLine($"  Name: {name}");
                }
            }

            // GroupByMultiple()
            // 22) Find placed students group by their genders
            var placedStudentsByGender = from student in students
                                         where student.isPlaced == true  // Filter only placed students
                                         group student by student.Gender // Group by Gender
                             into g // Create a group "g" of students by gender
                                         select new
                                         {
                                             Gender = g.Key,  // Access the Gender (the key of the group)
                                             Students = g.ToList() // The group of students that match the gender
                                         };

            Console.ReadKey();
        }
    }
}
