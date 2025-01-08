using System;
using BaseLibrary;

namespace BaseLibrary
{
    public class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Create an instance of MyDictionary to hold student ID and name
            /// </summary>
            MyDictionary<int, string> studentList = new MyDictionary<int, string>();

            /// <summary>
            /// Adding students to the dictionary
            /// </summary>
            studentList.AddToDictionary(1, "Priyank");
            studentList.AddToDictionary(2, "Anjali");
            studentList.AddToDictionary(3, "Suresh");
            studentList.AddToDictionary(4, "Madhu");
            studentList.AddToDictionary(5, "Nahii");

            /// <summary>
            /// Checking if a student with a specific ID exists in the dictionary.
            /// </summary>
            Console.WriteLine($"studentList.ContainsToDictionary(1): {studentList.ContainsToDictionary(1)}");

            /// <summary>
            /// Attempt to retrieve the value (student name) by ID.
            /// </summary>
            if (studentList.TryGetValueFromDictionary(3, out string studentName))
            {
                Console.WriteLine($"Student with ID 3 is {studentName}");
            }
            else
            {
                Console.WriteLine("Student with ID 3 not found.");
            }

            /// <summary>
            /// Check if a student name exists in the dictionary.
            /// </summary>
            bool hasStudent = studentList.ContainsValueInDictionary("Suresh");
            Console.WriteLine($"Is 'Suresh' in the dictionary? {hasStudent}");

            /// <summary>
            /// Removing a student from the dictionary by their ID.
            /// </summary>
            studentList.RemoveFromDictionary(2);

            /// <summary>
            /// Displaying all students in the dictionary.
            /// </summary>
            Console.WriteLine("Displaying all students in the dictionary:");
            foreach (var student in studentList)
            {
                Console.WriteLine($"ID: {student.Key}, Name: {student.Value}");
            }

            /// <summary>
            /// Clearing the dictionary.
            /// </summary>
            studentList.ClearDictionary();

            /// <summary>
            /// Displaying the dictionary after clearing.
            /// </summary>
            Console.WriteLine("\nDisplaying all students after clearing the dictionary:");
            if (studentList.Count == 0)
            {
                Console.WriteLine("The dictionary is empty.");
            }
            else
            {
                foreach (var student in studentList)
                {
                    Console.WriteLine($"ID: {student.Key}, Name: {student.Value}");
                }
            }

            Console.ReadKey();
        }
    }
}
