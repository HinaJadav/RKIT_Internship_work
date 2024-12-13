using System;
using System.IO;

class FileOperations
{
    /// <summary>
    /// This example demonstrates how to use the File class methods for creating, manipulating, and checking files in C#.
    /// </summary>
    static void Main()
    {
        string filePath = "example1.txt";
        string copyPath = "example_copy.txt";
        string replacePath = "replace_example.txt";
        string textToAppend = "\nThis is appended text.";
        string newText = "This is new content to overwrite the old one.";

        /// <summary>
        /// Creates or overwrites a file with the initial content.
        /// </summary>
        File.WriteAllText(filePath, "Initial file content.");
        Console.WriteLine("File created with initial content.");

        /// <summary>
        /// Appends text at the end of the existing file.
        /// </summary>
        // File.AppendText(filePath).Write(textToAppend);
        // Console.WriteLine("Text appended to the file.");

        /// <summary>
        /// Reads all text from the file and displays it.
        /// </summary>
        string fileContent = File.ReadAllText(filePath);
        Console.WriteLine("File content: \n" + fileContent);

        /// <summary>
        /// Copies the file to a new location.
        /// </summary>
        File.Copy(filePath, copyPath);
        Console.WriteLine("File copied to: " + copyPath);

        /// <summary>
        /// Checks whether the file exists at the specified location.
        /// </summary>
        if (File.Exists(copyPath))
        {
            Console.WriteLine("The copied file exists.");
        }

        /// <summary>
        /// Replaces the content of the original file with another file's content.
        /// </summary>
        File.WriteAllText(replacePath, "This is the replacement content.");
        File.Replace(replacePath, filePath, null); // Replaces original file
        Console.WriteLine("File replaced with new content from another file.");

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        File.Delete(replacePath);
        Console.WriteLine("File deleted: " + replacePath);

        /// <summary>
        /// Deletes the copied file.
        /// </summary>
        File.Delete(copyPath);
        Console.WriteLine("Copied file deleted: " + copyPath);

        /// <summary>
        /// Checks if the original file exists after deletion.
        /// </summary>
        if (!File.Exists(filePath))
        {
            Console.WriteLine("The original file no longer exists.");
        }
    }
}
