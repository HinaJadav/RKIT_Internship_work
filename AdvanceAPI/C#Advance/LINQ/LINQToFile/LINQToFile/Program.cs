using System;
using System.IO;
using System.Linq;

namespace LINQToFile
{
    public class Program
    {
        ///<summary>
        /// The main entry point for the application. This method writes content to a file,
        /// reads the file, extracts distinct words, and displays them in the console.
        ///</summary>
        static void Main(string[] args)
        {
            string filePath = "linqToFileText.txt";
            string fileContent = "Hello world! Welcome to LINQ file processing. LINQ makes data queries simple and powerful. " +
                     "With LINQ, you can work with collections, files, databases, and more. " +
                     "This is an example text file to demonstrate LINQ in action. Enjoy learning LINQ!";

            ///<summary>
            /// Writing the string content to the file at the specified file path.
            ///</summary>
            File.WriteAllText(filePath, fileContent);

            string filePath1 = "linqToFileText1.txt";
            string fileContent1 = "Hello world! Welcome to LINQ file processing. LINQ makes data queries simple and powerful. " +
                     "With LINQ, you can work with collections, files, databases, and more. ";

            ///<summary>
            /// Writing the string content to the file at the specified file path.
            ///</summary>
            File.WriteAllText(filePath1, fileContent1);

            ///<summary>
            /// Reading the entire file content, splitting it into words based on common delimiters
            /// (spaces, punctuation), and selecting distinct words.
            ///</summary>
            string[] distinctWordsFile1 = File.ReadAllText(filePath)
                .Split(new[] { ' ', '.', ',', '?', '!', ';', ':', '\r', '\n' }) // No StringSplitOptions
                .Where(word => !string.IsNullOrWhiteSpace(word)) // Exclude empty or whitespace entries
                .Distinct()  // Get distinct words
                .ToArray();  // Convert to array

            string[] distinctWordsFile2 = File.ReadAllText(filePath1)
                .Split(new[] { ' ', '.', ',', '?', '!', ';', ':', '\r', '\n' }) // No StringSplitOptions
                .Where(word => !string.IsNullOrWhiteSpace(word)) // Exclude empty or whitespace entries
                .Distinct()  // Get distinct words
                .ToArray();  // Convert to array

            ///<summary>
            /// Display the distinct words found in the first file to the console.
            ///</summary>
            Console.WriteLine($"Distinct words in the first file ({distinctWordsFile1.Count()} total):");
            foreach (var word in distinctWordsFile1)
            {
                Console.WriteLine(word);
            }

            ///<summary>
            /// Display the distinct words found in the second file to the console.
            ///</summary>
            Console.WriteLine($"\nDistinct words in the second file ({distinctWordsFile2.Count()} total):");
            foreach (var word in distinctWordsFile2)
            {
                Console.WriteLine(word);
            }

            // Exclude common words like (and, in, a, an, the) using Except()
            ///<summary>
            /// Get the distinct words from the first file excluding common words
            ///</summary>
            var commonExclusionWords = new[] { "and", "in", "a", "an", "the" };
            var filteredDistinctWordsFile1 = distinctWordsFile1
                .Except(commonExclusionWords, StringComparer.OrdinalIgnoreCase)
                .ToArray();

            Console.WriteLine($"\nDistinct words in the first file excluding common words:");
            foreach (var word in filteredDistinctWordsFile1)
            {
                Console.WriteLine(word);
            }

            // Find the set of common words between the two files using Intersect()
            ///<summary>
            /// Find common distinct words between the two files.
            ///</summary>
            var commonWordsBetweenFiles = distinctWordsFile1
                .Intersect(distinctWordsFile2, StringComparer.OrdinalIgnoreCase)
                .ToArray();

            Console.WriteLine($"\nCommon words between the two files ({commonWordsBetweenFiles.Count()} total):");
            foreach (var word in commonWordsBetweenFiles)
            {
                Console.WriteLine(word);
            }

            // Find all words used in both files using Union()
            ///<summary>
            /// Find all distinct words used across both files.
            ///</summary>
            var allDistinctWords = distinctWordsFile1
                .Union(distinctWordsFile2, StringComparer.OrdinalIgnoreCase)
                .ToArray();

            Console.WriteLine($"\nAll distinct words across both files ({allDistinctWords.Count()} total):");
            foreach (var word in allDistinctWords)
            {
                Console.WriteLine(word);
            }

            ///<summary>
            /// Wait for user input before closing the console application.
            ///</summary>
            Console.ReadKey();
        }
    }
}
