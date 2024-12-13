using System;
using System.Collections.Generic; // for List

namespace Arrays
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Array

            Console.WriteLine("Do easy Addition.");
            Console.Write("Enter input size: ");
            int n = int.Parse(Console.ReadLine());

            int[] inputs = new int[n];

            Console.WriteLine("Enter the inputs one by one:");
            for (int i = 0; i < n; ++i)
            {
                Console.Write($"Input {i + 1}: "); 
                inputs[i] = int.Parse(Console.ReadLine());
            }

            int total = 0;

            foreach (int input in inputs)
            {
                total += input;
            }

            Console.WriteLine($"The total sum is: {total}");

            // Array sorting

            Array.Sort(inputs);

            Console.Write("\nSorted Array: ");
            foreach (int input in inputs)
            {
                Console.Write($"{input}, ");
            }

            // Array reversal

            Array.Reverse(inputs);

            Console.Write("\nReversed Array: ");
            foreach (int input in inputs)
            {
                Console.Write($"{input}, ");
            }

            // Array cleaning 
            // In this process it replace all value present into array with that datatype default vaue like for integer default value is '0'

            // syntax: Array.Clear(arrayName, startIndex, size);
            // startIndex & size & (startIndex + size) <= array size
            Array.Clear(inputs, 0, n);
            // ex: Array.Clear(inputs, 5, 3) 
            // In output in "inputs" array it set '0' start from index 5 and updo startIndex+3

            // Another method:
                //for (int i = 0; i < n; ++i)
                //{
                //    inputs[i] = default; // "default" add default value of current data types
                //}

            Console.Write("\nCleaned Array: ");
            foreach (int input in inputs)
            {
                Console.Write($"{input}, ");
            }

        // Array IndexOf
        // syntax:
            // int Array.IndexOf(array, value);
            // int Array.IndexOf(array, value, startIndex);
            // int Array.IndexOf(array, value, startIndex, count);
                //array: The one-dimensional array to search.
                //value: The object to locate in the array.
                //startIndex(optional): The zero-based starting index of the search.
                //count(optional): The number of elements to search in the array, starting from startIndex.

            Console.WriteLine("\n\nEnter total number of students: ");
            int classSize;

            // try catch block
            try
            {
                classSize = int.Parse(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
                return;
            }

            Console.WriteLine("Enter student names: ");
            string[] studentNames = new string[classSize];

            for (int i = 0; i < classSize; ++i)
            {
                Console.Write($"{i + 1}). ");
                studentNames[i] = Console.ReadLine();
            }

            Console.WriteLine("\nFind your position in class!");
            Console.Write("Enter your name: ");
            string searchName = Console.ReadLine();

            int position = Array.FindIndex(studentNames, name =>
                string.Equals(name, searchName, StringComparison.OrdinalIgnoreCase));

            if (position == -1) // Array.Search() return -1 position when searched value is not found.
            {
                Console.WriteLine("Enter a valid name!");
            }
            else
            {
                Console.WriteLine($"{searchName} rank in class = {position + 1}");
            }

            // List
            // dynamic array

            Console.Write("\nList: ");
            List<int> listNum = new List<int>()
            {
                1,2,3,4,4,4
            };

            listNum.Add(6);

            foreach (int num in listNum)
            {
                Console.Write($"{num}, ");
            }

            listNum.Remove(3); // Remove the first occurrence of 3
            listNum.RemoveAll(num => num == 4); // Remove all occurrences of 4
            listNum.RemoveAt(0); // Remove element present at '0'

            Console.Write("\nList After remove operations: ");
            foreach (int num in listNum)
            {
                Console.Write($"{num}, ");
            }
            Console.ReadLine();

            // Multi-Dimensional Array
            int[,] multiDimArray = new int[3, 2]
            {
            { 1, 2 },
            { 3, 4 },
            { 5, 6 }
            };
            
            for (int i = 0; i < 3; i++) 
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write(multiDimArray[i, j] + " ");
                }
                Console.WriteLine();
            }

            // Jagged array
            int[][] jaggedArray = new int[3][]; 
            jaggedArray[0] = new int[] { 1, 2, 3 };   
            jaggedArray[1] = new int[] { 4, 5 };     
            jaggedArray[2] = new int[] { 6, 7, 8, 9 }; 

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write(jaggedArray[i][j] + " ");
                }
                Console.WriteLine();
            }

            // string as input in array
            Console.WriteLine("Enter a string:");
            string inputString = Console.ReadLine();

            // Split the string by spaces
            string[] words = inputString.Split(' ');

            // Print each word from the array
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}
