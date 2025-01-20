using Newtonsoft.Json;
using StudentMgmtSystem.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentMgmtSystem.BL
{
    public class S01Service
    {
        private readonly string _dataFilePath = "Data/S01Data.json";

        public List<YMS01> GetAll()
        {
            var jsonData = File.ReadAllText(_dataFilePath);

            List<YMS01> data = JsonConvert.DeserializeObject<List<YMS01>>(jsonData);

            return data;
        }
    }
}