using ServiceStack.DataAnnotations;
using System;

namespace FinalDemo.Models.POCO
{
    public class YMT01
    {
        [PrimaryKey]
        [AutoIncrement]
        public int T01F01 { get; set; } // team id

        [Required]
        public string T01F02 { get; set; } // team name

        [IgnoreOnUpdate]
        public DateTime T01F03 { get; set; } = DateTime.Now; // Team created Date

        [ForeignKey(typeof(YMM01))]
        public int T01F04 { get; set; } // member ID who created team
    }
}