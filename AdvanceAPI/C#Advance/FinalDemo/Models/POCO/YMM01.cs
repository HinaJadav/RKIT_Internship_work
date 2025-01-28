using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalDemo.Models.POCO
{
    // member entities
    public class YMM01
    {
        [Key]
        public int M01F01 { get; set; } // id

        [Required]
        [StringLength(50)]
        public string M01F02 { get; set; } // first name

        [Required]
        [StringLength(50)]
        public string M01F03 { get; set; } // last name

        [EmailAddress]
        public string M01F04 { get; set; } // email

        [MaxLength(10)]
        public int M01F05 { get; set; } // contact number

        [ForeignKey("YMR01")]
        public int M01F06 { get; set; } // role id

        [ForeignKey("YMT01")]
        public int M01F07 { get; set; } // team id

        public DateTime M01F08 { get; set; } // joining date

        public bool M01F09 { get; set; } // isActive member or not 
    }
}