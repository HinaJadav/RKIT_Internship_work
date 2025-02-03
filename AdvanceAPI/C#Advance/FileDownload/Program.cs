using System;
using System.IO;

namespace FileDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            // Call the method to simulate file download
            DownloadFile();

            
        }

        /// <summary>
        /// Simulates downloading a file from a server by copying it to a new location.
        /// </summary>
        public static void DownloadFile()
        {
            // Specify the file path for the file to download
            string sourceFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "MyFile.pdf");
            string destinationFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "DownloadedFile.pdf");

            if (File.Exists(sourceFilePath))
            {
                // Create the directory if it doesn't exist
                Directory.CreateDirectory(Path.GetDirectoryName(destinationFilePath));

                // Simulate file transmission by copying the file to the new location (download simulation)
                Console.WriteLine($"Simulating download: Copying file from {sourceFilePath} to {destinationFilePath}...");

                try
                {
                    // Copy the file (this simulates the download)
                    File.Copy(sourceFilePath, destinationFilePath, overwrite: true);

                    Console.WriteLine($"File downloaded successfully! File copied to: {destinationFilePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while downloading the file: {ex.Message}");
                }
            }
            else
            {
                // Handle file not found
                Console.WriteLine("File not found.");
            }

            Console.ReadKey();
        }
    }
}
