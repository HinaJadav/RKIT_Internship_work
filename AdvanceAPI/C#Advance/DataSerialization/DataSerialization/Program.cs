using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DataSerialization
{
    /// <summary>
    /// Represents a User with properties: Name, Email, and Password.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }
    }

    public class Program
    {
        /// <summary>
        /// Main method of the program. It demonstrates serializing a User object to JSON 
        /// and deserializing JSON data back into a User object using different serializers.
        /// </summary>
        static void Main(string[] args)
        {
            // Creating a User object with sample data
            User user1 = new User()
            {
                Name = "Priyank",       // User's name
                Email = "pu@gmail.com", // User's email
                Password = "pu$123"     // User's password
            };

            // *** Method 1: Using DataContractJsonSerializer ***

            Console.WriteLine("Using DataContractJsonSerializer:");

            // Creating a DataContractJsonSerializer to serialize the User object
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(User));

            // Creating a MemoryStream to hold the serialized JSON data
            MemoryStream memoryStream = new MemoryStream();

            // Serializing the User object to the MemoryStream
            jsonSerializer.WriteObject(memoryStream, user1);

            // Resetting the MemoryStream's position to the beginning for reading
            memoryStream.Position = 0;

            // Creating a StreamReader to read the serialized JSON data from the MemoryStream
            StreamReader streamReader = new StreamReader(memoryStream);

            // Reading the entire JSON data as a string
            string jsonString = streamReader.ReadToEnd();

            // Cleaning up the streams by closing them
            streamReader.Close();
            memoryStream.Close();

            // Output the serialized JSON string
            Console.WriteLine("Serialized JSON:\n" + jsonString);

            // Deserialization using DataContractJsonSerializer

            // JSON data string representing a User object
            string jsonData = "{\"Name\":\"Priyank\",\"Email\":\"pu@gmail.com\", \"Password\":\"pu$123\"}";

            // Convert the JSON string into a byte array using Unicode encoding and store it in a MemoryStream
            // This is necessary because the DataContractJsonSerializer reads from streams of bytes
            using (MemoryStream jsonDataStream = new MemoryStream(Encoding.Unicode.GetBytes(jsonData)))
            {
                // Create a DataContractJsonSerializer for the User type
                // This serializer will deserialize JSON data into a User object
                DataContractJsonSerializer jsonDeserializer = new DataContractJsonSerializer(typeof(User));

                // Deserialize the JSON data from the MemoryStream into a User object
                User deserializedUser = (User)jsonDeserializer.ReadObject(jsonDataStream);

                // Display the deserialized User object's properties
                Console.WriteLine("Deserialized User (DataContractJsonSerializer):");
                Console.WriteLine($"Name: {deserializedUser.Name}, Email: {deserializedUser.Email}, Password: {deserializedUser.Password}");
            }


            // *** Method 2: Using JavaScriptSerializer *** --> not supported into this project version 

            // *** Method 3: Using Json.NET (Newtonsoft.Json) ***

            Console.WriteLine("\nUsing Json.NET (Newtonsoft.Json):");

            // Serializing the User object using Json.NET
            string jsonNetString = JsonConvert.SerializeObject(user1);

            // Output the serialized JSON string
            Console.WriteLine("Serialized JSON:\n" + jsonNetString);

            // Deserialization using Json.NET
            User jsonNetDeserializedUser = JsonConvert.DeserializeObject<User>(jsonNetString);
            Console.WriteLine("Deserialized User (Json.NET):");
            Console.WriteLine($"Name: {jsonNetDeserializedUser.Name}, Email: {jsonNetDeserializedUser.Email}, Password: {jsonNetDeserializedUser.Password}");



            //---------------------------------------------------------

            // *** Method 1: Using XmlSerializer ***

            /// <summary>
            /// Demonstrates how to serialize a list of User objects into an XML file using the XmlSerializer.
            /// </summary>
            /// <remarks>
            /// This code serializes a predefined list of User objects into an XML file stored in the application's current directory.
            /// </remarks>

            // Define a list of User objects with sample data
            List<User> usersList = new List<User>
            {
                new User
                {
                    Name = "Anjali",        // Name of the user
                    Email = "anu@gmail.com", // Email address of the user
                    Password = "anu$123"     // Password of the user
                },
                new User
                {
                    Name = "Nahi",         // Name of the user
                    Email = "nahi@gmail.com", // Email address of the user
                    Password = "Nahi123@"     // Password of the user
                }
            };

            // Specify the file path to store the serialized XML data
            string path = Path.Combine(Directory.GetCurrentDirectory(), "UserData.xml");

            // Create an XmlSerializer for the type List<User>
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));

            // Serialize the list of users into the XML file using FileStream
            using (FileStream fileStream = File.Create(path))
            {
                xmlSerializer.Serialize(fileStream, usersList);
            }

            // Notify the user that serialization is complete
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Data is serialized successfully into XML file.");

            // Deserialize the XML data back into a list of User objects
            List<User> deserializedUsers;
            using (FileStream fileStreamDeserializer = File.Open(path, FileMode.Open))
            {
                deserializedUsers = xmlSerializer.Deserialize(fileStreamDeserializer) as List<User>;
            }

            // Notify the user that deserialization is complete
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Data is deserialized successfully from the XML file.");

            // Display the deserialized data
            Console.WriteLine("\nDeserialized Users:");
            foreach (User user in deserializedUsers)
            {
                Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Password: {user.Password}");
            }


            // *** Method 2: Using DataContractSerializer ***

            /// <summary>
            /// Demonstrates the serialization and deserialization of a list of User objects using DataContractSerializer.
            /// The data is serialized into an XML file and then deserialized back into a list of User objects.
            /// </summary>

            // Specify the file path to store the serialized XML data
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserDataByDataContractSerializer.xml");

            // Create a DataContractSerializer instance for the User type
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(List<User>));

            // Serialize the list of users into the XML file
            using (FileStream stream = File.Create(filePath))
            {
                dataContractSerializer.WriteObject(stream, usersList);
            }

            // Notify the user that serialization is complete
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Data is serialized using DataContractSerializer successfully into XML file.");

            // Deserialize the XML data back into a list of User objects
            List<User> deserializedUsers1;
            using (FileStream streamDeserialized = File.Open(filePath, FileMode.Open))
            {
                deserializedUsers1 = dataContractSerializer.ReadObject(streamDeserialized) as List<User>;
            }

            // Notify the user that deserialization is complete
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Data is deserialized using DataContractSerializer successfully from the XML file.");

            // Display the deserialized data
            Console.WriteLine("\nDeserialized Users:");
            foreach (User user in deserializedUsers1)
            {
                Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Password: {user.Password}");
            }

            // Wait for user input before closing the console
            Console.ReadKey();
        }
    }
}
