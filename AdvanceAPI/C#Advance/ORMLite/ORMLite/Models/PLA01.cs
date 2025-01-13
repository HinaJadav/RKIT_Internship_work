using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORMLite.Models
{
    // Table for stores player's basic information
    public class PLA01
    {
        //  Id
        public int A01F01 { get; set; }
        // Name
        public string A01F02 { get; set; }
        // age
        public int A01F03 { get; set; }
        // team name
        public string A01F04 { get; set; }
        // game name
        public string A01F05 { get; set; }

        // country 
        public string A01F06 { get; set;  }
    }
}