using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Text.Json;
using System.Collections.Generic;

namespace FileSystemInDepth
{
    /// <summary>
    /// Topics:
    /// FileStream
    /// StringReader & StringWriter
    /// ByteReader & ByteWriter
    /// </summary>
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
                catch (UnauthorizedAccessException ex) // This exception occurs when some times some drive has not some access like read or write for security etc.
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

                    // File Locking Scenario
                    PerformFileLockingScenario(subDirectoryPath);

                    // For read and write JSON data
                    WriteAndReadJsonFile();

                    // For read and write dictionary format data
                    WriteAndReadKeyValueJsonFile();

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


            // file access scenario
            string filePath = @"F:\AAA\BBB\lockedFile.txt";
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    Console.WriteLine("File opened successfully!");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File is locked: {ex.Message}");
            }


            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// User model for JSON serialization.
        /// </summary>
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        // A

        /// <summary>
        /// Demonstrates writing and reading JSON data using file operations.
        /// </summary>
        private static void WriteAndReadJsonFile()
        {
            string filePath = @"F:/AAA/BBB/userData.json";

            // Create a sample object
            var user = new User
            {
                Id = 1,
                Name = "Priyank Shah",
                Email = "priyank.shah@example.com"
            };

            // Writing JSON data to a file
            string jsonData = JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonData);
            Console.WriteLine("\nJSON data written to file 'userData.json'");

            // Reading JSON data from a file
            string jsonFromFile = File.ReadAllText(filePath);
            User userRead = JsonSerializer.Deserialize<User>(jsonFromFile);

            // Displaying the data read from the file
            Console.WriteLine("\nJSON data read from file 'userData.json':");
            Console.WriteLine($"ID: {userRead.Id}");
            Console.WriteLine($"Name: {userRead.Name}");
            Console.WriteLine($"Email: {userRead.Email}");
        }

        // A

        /// <summary>
        /// Demonstrates writing and reading JSON data with key-value pairs using file operations.
        /// </summary>
        private static void WriteAndReadKeyValueJsonFile()
        {
            string filePath = @"F:/AAA/BBB/keyValueData.json";

            try
            {
                // Creating a dictionary with key-value pairs
                var keyValueData = new List<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Anu Shah"),
                    new KeyValuePair<int, string>(2, "Nahii Shah"),
                    new KeyValuePair<int, string>(3, "Suresh Jadav")
                };

                // Writing JSON data to a file
                string jsonData = JsonSerializer.Serialize(keyValueData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine("\nKey-Value JSON data written to file 'keyValueData.json'");

                // Reading JSON data from the file
                string jsonFromFile = File.ReadAllText(filePath);
                var keyValueRead = JsonSerializer.Deserialize<List<KeyValuePair<int, string>>>(jsonFromFile);

                // Displaying the data read from the file
                Console.WriteLine("\nKey-Value JSON data read from file 'keyValueData.json':");
                foreach (var item in keyValueRead)
                {
                    Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
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
                // FileMode.Create: Creates a new file or overwrites if it exists.
                // 
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

        // A

        /// <summary>
        /// Demonstrates file locking by creating and locking a file.
        /// Prevents other processes from accessing the file while in use.
        /// </summary>
        /// <param name="directoryPath">The directory path where the locked file will be created.</param>
        private static void PerformFileLockingScenario(string directoryPath)
        {
            string filePath = Path.Combine(directoryPath, "file.txt");
            try
            {
                // Open a file with exclusive access, preventing other processes from using it.
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    Console.WriteLine("File is locked and being accessed by this process.");
                    Thread.Sleep(10000);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IOException: Another process is using the file. Error: {ex.Message}");
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
