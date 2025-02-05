using System;
using System.Collections.Generic;
using System.Linq;

// linq to link of objects
namespace LINQToObject
{
    public class Program
    {
        static void Main(string[] args)
        {
            // List of students object
            List<Student> students = new List<Student>
            {
                new Student(1, "Aarav Sharma", 8.5, 120000, 600000, true, "Male", 1),
                new Student(2, "Ishita Verma", 9.2, 110000, 750000, true, "Female", 2),
                new Student(3, "Rohan Singh", 7.8, 115000, 550000, true, "Male", 3),
                new Student(4, "Ananya Gupta", 9.2, 118000, 0, false, "Female", 1),
                new Student(5, "Vivaan Patel", 9.0, 112000, 800000, true, "Male", 2),
                new Student(6, "Sanya Joshi", 7.5, 125000, 0, false, "Female", 3),
                new Student(7, "Kabir Kumar", 8.8, 119000, 720000, true, "Male", 1),
                new Student(8, "Priya Mehta", 8.3, 121000, 0, false, "Female", 2),
                new Student(9, "Aditya Roy", 9.1, 116000, 780000, true, "Male", 3),
                new Student(10, "Riya Jain", 6.0, 122000, 0, false, "Female", 1)
            };
            // female -> max cpi -> max package 
            Student ans = students.Where(stu => stu.Gender == "Female").OrderByDescending(stu => stu.CPI).ThenByDescending(stu => stu.Package).FirstOrDefault();

            Console.WriteLine(ans.Name);

            // Print all student information 
            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, CPI: {student.CPI}, " +
                                  $"Fees: {student.Fees}, Package: {student.Package}, Placed: {student.isPlaced}, " +
                                  $"Gender: {student.Gender}, DepartmentID: {student.DepartmentID}");
            }

            // Queries using LINQ 

            /// <summary>
            /// Aggregates:
            /// 1) Find total amount of Fees that College collects from all students using Sum().
            /// </summary>
            int totalFees = students.Sum(student => student.Fees);
            Console.WriteLine($"\nTotal Fees Collected: {totalFees} Rs.");

            /// <summary>
            /// 2) Find average package amount of college using Average().
            /// </summary>
            double averagePackage = students.Average(student => student.Package);
            Console.WriteLine($"\nAverage package of College: {averagePackage} Rs.");

            /// <summary>
            /// 3) Count how many students are placed using Count().
            /// </summary>
            int noOfStudentPlaced = students.Count(student => student.isPlaced == true);
            Console.WriteLine($"\nNumber of student placed: {noOfStudentPlaced}");

            /// <summary>
            /// 4) Find the student with the maximum package using Method Syntax (Max()).
            /// </summary>
            string maximumPackageStudentName = students
                .Where(student => student.Package == students.Max(s => s.Package))
                .Select(student => student.Name).FirstOrDefault();
            Console.WriteLine($"\nStudent with the maximum package: {maximumPackageStudentName}");

            /// <summary>
            /// 5) Find the student with the minimum package using Query Syntax (Min()).
            /// </summary>
            string minimumPackageStudentName = (from student in students where student.Package == (from s in students select s.Package).Min() select student.Name).FirstOrDefault();
            Console.WriteLine($"\nStudent with the minimum package: {minimumPackageStudentName}");

            /// <summary>
            /// 6) Check if all students are placed using All().
            /// </summary>
            bool isAllPlaced = students.All(student => student.isPlaced == true);
            Console.WriteLine($"\nCheck that all college students are placed or not: {isAllPlaced}");

            /// <summary>
            /// 7) Check if any student has 10 CPI using Any().
            /// </summary>
            bool isAnyStudentHasFullCPI = (from student in students
                                           where student.CPI == 10.0
                                           select student).Any();
            Console.WriteLine($"\nCheck that any college students have 10.0 CPI: {isAnyStudentHasFullCPI}");

            /// <summary>
            /// 8) Create a dictionary of students with their student Id using ToDictionary().
            /// </summary>
            Dictionary<int, string> studentsDictionary = students.ToDictionary(student => student.Id, student => student.Name);
            Console.WriteLine($"\nAll students name with their Ids:");
            foreach (var student in studentsDictionary)
            {
                Console.WriteLine($"ID: {student.Key}, Name: {student.Value}");
            }

            /// <summary>
            /// 9) Generate a lookup table based on student DepartmentID and Name using ToLookup().
            /// </summary>
            ILookup<int, string> studentsLookup = students.ToLookup(student => student.DepartmentID, student => student.Name);

            Console.WriteLine($"\nGenerate Lookup table: ");
            foreach (var department in studentsLookup)
            {
                Console.WriteLine($"Department ID: {department.Key}");
                foreach (string name in department)
                {
                    Console.WriteLine($"    Name: {name}");
                }
            }

            /// <summary>
            /// 10) Order students by package (descending) and name (ascending) using OrderByDescending() and ThenBy().
            /// </summary>
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

            /// <summary>
            /// LINQ to Array Operations:
            /// 11) Find the student with the 3rd highest package using ElementAt().
            /// </summary>
            string thirdHighestPackageStudent = orderedPlacedStudentNames.ElementAt(2);
            Console.WriteLine($"\nStudent with the 3rd highest package: {thirdHighestPackageStudent}");

            /// <summary>
            /// 12) Find the student with the highest package using First().
            /// </summary>
            string highestPackageStudent = orderedPlacedStudentNames.First();
            Console.WriteLine($"\nStudent with the highest package: {highestPackageStudent}");

            /// <summary>
            /// 13) Find the student with the highest package or default if none using FirstOrDefault().
            /// </summary>
            string highestPackageStudentOrDefault = orderedPlacedStudentNames.FirstOrDefault();
            Console.WriteLine($"\nStudent with the highest package (or default): {highestPackageStudentOrDefault}");

            /// <summary>
            /// 14) Find the student with the lowest package using Last().
            /// </summary>
            string lowestPackageStudent = orderedPlacedStudentNames.Last();
            Console.WriteLine($"\nStudent with the lowest package: {lowestPackageStudent}");

            /// <summary>
            /// 15) Find the student with the lowest package or default if none using LastOrDefault().
            /// </summary>
            string lowestPackageStudentOrDefault = orderedPlacedStudentNames.LastOrDefault();
            Console.WriteLine($"\nStudent with the lowest package (or default): {lowestPackageStudentOrDefault}");

            /// <summary>
            /// 16) Get students excluding the top 3 by package using Skip().
            /// </summary>
            string[] skippedPlacedStudents = orderedPlacedStudentNames.Skip(3).ToArray();
            Console.WriteLine("\nStudents excluding the top 3 by package:");
            foreach (string student in skippedPlacedStudents)
            {
                Console.WriteLine(student);
            }

            /// <summary>
            /// 17) Get the list of the top 3 students by package using Take().
            /// </summary>
            string[] topThreePlacedStudents = orderedPlacedStudentNames.Take(3).ToArray();
            Console.WriteLine("\nTop 3 students by package:");
            foreach (string student in topThreePlacedStudents)
            {
                Console.WriteLine(student);
            }

            /// <summary>
            /// 18) Check if "Vivaan" is in the list of placed students using Contains().
            /// </summary>
            bool isVivaanPlaced = orderedPlacedStudentNames.Contains("Vivaan");
            Console.WriteLine($"Is Vivaan placed? {isVivaanPlaced}");

            /// <summary>
            /// 19) Add "Sanya Joshi" to the list of placed students using Concat().
            /// </summary>
            string[] updatedPlacedStudentNames = orderedPlacedStudentNames.Concat(new[] { "Sanya Joshi" }).ToArray();
            Console.WriteLine("\nUpdated list of placed students (including Sanya Joshi):");
            foreach (string student in updatedPlacedStudentNames)
            {
                Console.WriteLine(student);
            }

            /// <summary>
            /// 20) Find students with a package between 400,000 and 600,000 using Where().
            /// </summary>
            string[] packageRangeStudentNames = students
                .Where(student => student.isPlaced && student.Package >= 400000 && student.Package <= 600000)
                .Select(student => student.Name)
                .ToArray();

            Console.WriteLine("\nStudents with a package between 400,000 and 600,000:");
            foreach (string name in packageRangeStudentNames)
            {
                Console.WriteLine(name);
            }

            /// <summary>
            /// 21) Group students by Gender using GroupBy().
            /// </summary>
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

            /// <summary>
            /// 22) Find placed students grouped by their gender using GroupBy() for placed students.
            /// </summary>
            var placedStudentsByGender = from student in students
                                         where student.isPlaced == true
                                         group student by student.Gender
                             into g
                                         select new
                                         {
                                             Gender = g.Key,
                                             Students = g.ToList()
                                         };

            Console.ReadKey();
        }
    }
}
