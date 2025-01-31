using ServiceStack.DataAnnotations;
using System;

namespace FinalDemo.Models.POCO
{
    // member entities
    public class YMM01
    {
        [PrimaryKey]
        [AutoIncrement]
        public int M01F01 { get; set; } // id

        [Required]
        [StringLength(50)]
        public string M01F02 { get; set; } // full name


        [Unique]
        public string M01F03 { get; set; } // email

        [DecimalLength(10, 0)] // Precision 10, Scale 0 (whole numbers)
        public decimal M01F04 { get; set; } // contact number

      

        [IgnoreOnUpdate]
        public DateTime M01F07 { get; set; } // joining date

        public bool M01F08 { get; set; } // isActive member or not 

        [Required]
        public string M01F09 { get; set; } // member password
    }
}
