using System;
using System.IO;
using System.Text;

namespace FileSystemInDepth
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Retrieve all drives on the system.
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                Console.WriteLine("Available drive partitions on your computer:");

                foreach (DriveInfo drive in allDrives)
                {
                    Console.WriteLine($"Drive Name: {drive.Name}");
                }

                // Prompt the user for a specific drive to get detailed information.
                Console.Write("Enter the drive letter to view detailed information (e.g., C:): ");
                string userSelectedDrive = Console.ReadLine();

                // Validate user input
                if (string.IsNullOrWhiteSpace(userSelectedDrive))
                {
                    Console.WriteLine("Invalid input. Please enter a valid drive letter.");
                    return;
                }

                try
                {
                    // Retrieve detailed information about the selected drive.
                    DriveInfo selectedDriveInfo = new DriveInfo(userSelectedDrive);

                    if (selectedDriveInfo.IsReady)
                    {
                        // Prepare detailed information about the drive as a string
                        string driveDetails = $"Detailed Information for Drive {selectedDriveInfo.Name}:\n" +
                                              $"Total Space: {selectedDriveInfo.TotalSize} bytes\n" +
                                              $"Free Space: {selectedDriveInfo.TotalFreeSpace} bytes\n" +
                                              $"Drive Format: {selectedDriveInfo.DriveFormat}\n" +
                                              $"Volume Label: {selectedDriveInfo.VolumeLabel}\n" +
                                              $"Drive Type: {selectedDriveInfo.DriveType}\n" +
                                              $"Root Directory: {selectedDriveInfo.RootDirectory}\n";

                        // Use StringWriter to write drive details to a string
                        using (StringWriter stringWriter = new StringWriter())
                        {
                            stringWriter.Write(driveDetails);
                            Console.WriteLine("\nDrive Details Written to StringWriter:");
                            Console.WriteLine(stringWriter.ToString());
                        }

                        // Use StringReader to read the drive details back
                        using (StringReader stringReader = new StringReader(driveDetails))
                        {
                            Console.WriteLine("\nReading Drive Details Using StringReader:");
                            string line;
                            while ((line = stringReader.ReadLine()) != null)
                            {
                                Console.WriteLine(line);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Drive {userSelectedDrive} is not ready.");
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Access to the drive is denied. Error: {ex.Message}");
                }
                catch (DriveNotFoundException ex)
                {
                    Console.WriteLine($"The specified drive was not found. Error: {ex.Message}");
                }

                // Example: Creating a new directory and subdirectory
                string parentDirectoryPath = @"F:\AAA";
                string subDirectoryName = "BBB";
                string subDirectoryPath = Path.Combine(parentDirectoryPath, subDirectoryName);

                try
                {
                    // Create or validate the parent directory
                    DirectoryInfo parentDirectory = new DirectoryInfo(parentDirectoryPath);
                    if (!parentDirectory.Exists)
                    {
                        parentDirectory.Create();
                        Console.WriteLine($"Parent directory created: {parentDirectoryPath}");
                    }
                    else
                    {
                        Console.WriteLine($"Parent directory already exists: {parentDirectoryPath}");
                    }

                    // Create or validate the subdirectory
                    DirectoryInfo subDirectory = new DirectoryInfo(subDirectoryPath);
                    if (!subDirectory.Exists)
                    {
                        subDirectory.Create();
                        Console.WriteLine($"Subdirectory created: {subDirectoryPath}");
                    }
                    else
                    {
                        Console.WriteLine($"Subdirectory already exists: {subDirectoryPath}");
                    }

                    // FileStream functionality
                    PerformFileStreamOperations(subDirectoryPath);

                    // BinaryWriter and BinaryReader functionality (small addition)
                    WriteAndReadBinaryFile();

                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Permission denied. Error: {ex.Message}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"I/O error occurred. Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // General exception handling for unexpected errors
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                // General exception handling
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates FileStream operations: writing data to a file and reading it back.
        /// </summary>
        /// <param name="directoryPath">The directory path where the file will be created.</param>
        private static void PerformFileStreamOperations(string directoryPath)
        {
            string filePath = Path.Combine(directoryPath, "file.txt");
            string dataToWrite = "This is the first data written using FileStream.";

            try
            {
                // Write data to the file using FileStream
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    byte[] dataBytes = Encoding.Default.GetBytes(dataToWrite);
                    fileStream.Write(dataBytes, 0, dataBytes.Length);
                    fileStream.Position = 0; // Reset position to the beginning for reading

                    // Read data back from the file
                    byte[] readBuffer = new byte[dataBytes.Length];
                    fileStream.Read(readBuffer, 0, readBuffer.Length);
                    string readData = Encoding.Default.GetString(readBuffer);

                    Console.WriteLine($"\nFileStream Operations:");
                    Console.WriteLine($"Data written to file: {dataToWrite}");
                    Console.WriteLine($"Data read from file: {readData}");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Permission denied while using FileStream. Error: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O error occurred during FileStream operations. Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates writing and reading data using BinaryWriter and BinaryReader.
        /// </summary>
        private static void WriteAndReadBinaryFile()
        {
            string filePath = @"F:/AAA/BBB/binaryDataFile.txt";

            // Writing data using BinaryWriter
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                int x = 007;
                string str = "hello binary data.";

                writer.Write(x);   // Write integer
                writer.Write(str); // Write string
                Console.WriteLine("\nData written to binary file 'binaryDataFile.text'");
            }

            // Reading data using BinaryReader
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int xRead = reader.ReadInt32();  // Read integer
                string strRead = reader.ReadString(); // Read string

                Console.WriteLine($"\nData read from binary file 'binaryDataFile.text':");
                Console.WriteLine($"Integer: {xRead}");
                Console.WriteLine($"String: {strRead}");
            }
        }
    }
}
