using FinalDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalDemo.BL
{
    public class S01Service
    {
        private readonly string _dataFilePath;

        public S01Service()
        {
            // Set the file path for the data file
            _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BL", "S01Data.json");
        }

        /// <summary>
        /// Reads data from the JSON file.
        /// </summary>
        /// <returns>A list of YMS01 objects.</returns>
        private List<YMS01> ReadData()
        {
            // If the file doesn't exist, return an empty list
            if (!File.Exists(_dataFilePath))
            {
                return new List<YMS01>();
            }

            var jsonData = File.ReadAllText(_dataFilePath);
            List<YMS01> data = JsonConvert.DeserializeObject<List<YMS01>>(jsonData) ?? new List<YMS01>();
            return data;
        }

        /// <summary>
        /// Writes the provided data to the JSON file.
        /// </summary>
        /// <param name="data">The data to write to the file.</param>
        private void WriteData(List<YMS01> data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_dataFilePath, jsonData);
        }

        /// <summary>
        /// Gets all data.
        /// </summary>
        /// <returns>A list of all YMS01 data.</returns>
        public List<YMS01> GetAll()
        {
            return ReadData();
        }

        /// <summary>
        /// Gets data by its ID.
        /// </summary>
        /// <param name="id">The ID of the data to retrieve.</param>
        /// <returns>The YMS01 data with the specified ID, or null if not found.</returns>
        public YMS01 GetDataById(int id)
        {
            List<YMS01> data = ReadData();
            return data.FirstOrDefault(s01 => s01.S01f01 == id);
        }

        /// <summary>
        /// Adds new data.
        /// </summary>
        /// <param name="newStudent">The new YMS01 data to add.</param>
        public void Add(YMS01 newStudent)
        {
            List<YMS01> data = ReadData();

            // Auto increment ID value
            newStudent.S01f01 = data.Any() ? data.Max(s01 => s01.S01f01) + 1 : 1;

            data.Add(newStudent);
            WriteData(data);
        }

        /// <summary>
        /// Updates existing data.
        /// </summary>
        /// <param name="id">The ID of the data to update.</param>
        /// <param name="updatedStudent">The updated YMS01 data.</param>
        /// <returns>True if the data was successfully updated, otherwise false.</returns>
        public bool Update(int id, YMS01 updatedStudent)
        {
            List<YMS01> data = ReadData();
            YMS01 findData = data.FirstOrDefault(s01 => s01.S01f01 == id);

            if (findData == null)
            {
                return false; // Data not found
            }

            // Update fields
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

        /// <summary>
        /// Deletes data by its ID.
        /// </summary>
        /// <param name="id">The ID of the data to delete.</param>
        /// <returns>True if the data was successfully deleted, otherwise false.</returns>
        public bool Delete(int id)
        {
            List<YMS01> data = ReadData();
            YMS01 student = data.FirstOrDefault(s01 => s01.S01f01 == id);

            if (student == null)
            {
                return false; // Data not found
            }

            data.Remove(student);
            WriteData(data);
            return true;
        }



    }
}
