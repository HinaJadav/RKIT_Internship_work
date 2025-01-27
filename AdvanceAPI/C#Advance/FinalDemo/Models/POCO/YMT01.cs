using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinalDemo.Models.POCO
{
    public class YMT01
    {
        [Key]
        public int T01F01 { get; set; } // team id

        [Required]
        public string T01F02 { get; set; } // team name

        public DateTime T01F03 { get; set; } = DateTime.Now; // Team created Date

        [ForeignKey("YMM01")]
        public int T01F04 { get; set; } // momber ID who created team
    }
}