using ServiceStack.DataAnnotations;

namespace FinalDemo.Models.POCO
{
    // role entities 
    public class YMR01
    {
        [PrimaryKey]
        public int R01F01 { get; set; } // Role ID

        [Required]
        [StringLength(50)]
        public string R02F02 { get; set; } // Role Name

        [Required]
        [StringLength(8)] // password length = 8 only 
        public string R03F03 { get; set; } // Role Password
    }
}