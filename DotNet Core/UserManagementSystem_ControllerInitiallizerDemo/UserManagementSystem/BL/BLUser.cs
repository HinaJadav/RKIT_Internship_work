using ControllerInitializationDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ControllerInitializationDemo.BL
{
    /// <summary>
    /// Business logic layer for managing user data using a JSON file as storage.
    /// </summary>
    public class BLUser : BLIUser
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserData.json");

        /// <summary>
        /// Initializes a new instance of the <see cref="BLUser"/> class and ensures the user data file exists.
        /// </summary>
        public BLUser()
        {
            Console.WriteLine("Using file path: " + Path.GetFullPath(_filePath));
            EnsureFileExists();
        }

        /// <summary>
        /// Ensures the user data file exists, creating an empty array if necessary.
        /// </summary>
        private void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        /// <summary>
        /// Reads the list of users from the JSON file.
        /// </summary>
        /// <returns>A list of users.</returns>
        private List<User> ReadUsersFromFile()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
        }

        /// <summary>
        /// Writes the list of users to the JSON file.
        /// </summary>
        /// <param name="users">The list of users to write.</param>
        private void WriteUsersToFile(List<User> users)
        {
            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A collection of users.</returns>
        public IEnumerable<User> GetAllUsers()
        {
            return ReadUsersFromFile();
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user object if found; otherwise, null.</returns>
        public User GetUserById(int id)
        {
            var users = ReadUsersFromFile();
            return users.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Creates a new user and assigns a unique ID.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        /// <returns>The created user object.</returns>
        public User CreateUser(User user)
        {
            var users = ReadUsersFromFile();
            user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            WriteUsersToFile(users);
            return user;
        }

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>The updated user object if found; otherwise, null.</returns>
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

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>True if the user was deleted; otherwise, false.</returns>
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
