using Newtonsoft.Json;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentManagementSystem.BL
{
    public class S01Service
    {
        private readonly string _dataFilePath;

        public S01Service()
        {
            _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BL", "S01Data.json");
        }

        // Centralized method to read data from the file
        private List<YMS01> ReadData()
        {
            if(!File.Exists(_dataFilePath)) {
                return new List<YMS01>();
            }

            var jsonData = File.ReadAllText(_dataFilePath);

            List<YMS01> data = JsonConvert.DeserializeObject<List<YMS01>>(jsonData) ?? new List<YMS01>();

            return data;
        }

        private void WriteData(List<YMS01> data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_dataFilePath, jsonData);
        }

        // In a real app, this could be a database or other data store
        private readonly List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "password123" }
        };

        public bool AuthenticateUser(string username, string password)
        {
            // Find the user by username
            var user = _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                return false; // User not found
            }

            // Check if the password matches
            return user.Password == password; // In real-world apps, use hashed passwords and compare hash
        }

        // Get all data
        public List<YMS01> GetAll()
        {
            return ReadData();
        }

        // Get data by id
        public YMS01 GetDataById(int id)
        {
            List<YMS01> data = ReadData();

            YMS01 findData = data.FirstOrDefault(s01 => s01.S01f01 == id);

            return findData;
        }

        // Add data
        public void Add(YMS01 newStudent)
        {
            List<YMS01> data = ReadData();

            newStudent.S01f01 = data.Any() ? data.Max(s01 => s01.S01f01) + 1 : 1; // Auto increment Id value

            data.Add(newStudent);

            WriteData(data);
        }

        // Update Data
        public bool Update(int id, YMS01 updatedStudent)
        {
            List<YMS01> data = ReadData();

            YMS01 findData = data.FirstOrDefault(s01 => s01.S01f01 == id);

            if(findData == null)
            {
                return false;
            }

            findData.S01f02 = updatedStudent.S01f02;
            findData.S01f03 = updatedStudent.S01f03;
            findData.S01f04 = updatedStudent.S01f04;
            findData.S01f05 = updatedStudent.S01f05;
            findData.S01f06 = updatedStudent.S01f06;
            findData.S01f07 = updatedStudent.S01f07;
            findData.S01f08 = updatedStudent.S01f08;
            findData.S01f09 = updatedStudent.S01f09;
            findData.S01f10 = updatedStudent.S01f10;
            findData.S01f11 = updatedStudent.S01f11;
            findData.S01f12 = updatedStudent.S01f12;

            WriteData(data);

            return true;
        }

        // Delete Data

        public bool Delete(int id)
        {
            List<YMS01> data = ReadData();

            YMS01 student = data.FirstOrDefault(s01 => s01.S01f01 == id);

            if(student == null)
            {
                return false;
            }

            data.Remove(student); 
            
            WriteData(data);
            
            return true;
        }
    }
}
