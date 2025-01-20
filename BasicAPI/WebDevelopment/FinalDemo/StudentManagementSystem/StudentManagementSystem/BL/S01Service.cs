using Newtonsoft.Json;
using StudentManagementSystem.Models;
using System.Collections.Generic;
using System.IO;

namespace StudentManagementSystem.BL
{
    public class S01Service
    {
        private readonly string _dataFilePath = "S01Data.json";

        public List<YMS01> GetAll()
        {
            var jsonData = File.ReadAllText(_dataFilePath);

            List<YMS01> data = JsonConvert.DeserializeObject<List<YMS01>>(jsonData);

            return data;
        }

    }
}