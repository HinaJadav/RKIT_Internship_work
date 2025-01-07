using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
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
            string jsonData = "{\"Name\":\"Priyank\",\"Email\":\"pu@gmail.com\", \"Password\":\"asd\"}";
            using (MemoryStream jsonDataStream = new MemoryStream(Encoding.Unicode.GetBytes(jsonData)))
            {
                DataContractJsonSerializer jsonDeserializer = new DataContractJsonSerializer(typeof(User));
                User deserializedUser = (User)jsonDeserializer.ReadObject(jsonDataStream);
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

            // Wait for user input before closing the console
            Console.ReadKey();
        }
    }
}
