using System;

namespace FinalDemo.Models.DTO
{
    public class DTOYMM01
    {
        public int M01101 { get; set; } // id 
        public string M01102 { get; set; } // member full name (fn + ln)
        
        public string M01103 { get; set; } // email
        public string M01104 { get; set; } // contact number
        
        public DateTime M01107 { get; set; } // joining date
        public int M01108 { get; set; } // isActive member or not

        public string M01109 { get; set; } // member password
    }
}