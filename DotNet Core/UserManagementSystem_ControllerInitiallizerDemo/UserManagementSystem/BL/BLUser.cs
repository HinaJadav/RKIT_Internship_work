using ControllerInitializationDemo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControllerInitializationDemo.BL
{
    public class BLUser : BLIUser
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserData.json");


        public BLUser()
        {
            Console.WriteLine("Using file path: " + Path.GetFullPath(_filePath));
            EnsureFileExists();
        }

        // Ensure the file exists with an empty array if it doesn't exist
        private void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        // Read users from the file
        private List<User> ReadUsersFromFile()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
        }

        // Write users to the file
        private void WriteUsersToFile(List<User> users)
        {
            var jsonData = JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return ReadUsersFromFile();
        }

        public User GetUserById(int id)
        {
            var users = ReadUsersFromFile();
            return users.FirstOrDefault(u => u.Id == id);
        }

        public User CreateUser(User user)
        {
            var users = ReadUsersFromFile();
            user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            WriteUsersToFile(users);
            return user;
        }

        public User UpdateUser(int id, User user)
        {
            var users = ReadUsersFromFile();
            var existingUser = users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Password = user.Password;
                WriteUsersToFile(users);
            }
            return existingUser;
        }

        public bool DeleteUser(int id)
        {
            var users = ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
                WriteUsersToFile(users);
                return true;
            }
            return false;
        }
    }
}
