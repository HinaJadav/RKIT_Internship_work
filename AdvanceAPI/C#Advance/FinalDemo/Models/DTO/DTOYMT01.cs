using System;

namespace FinalDemo.Models.DTO
{
    public class DTOYMT01
    {
        public int T01101 { get; set; } // team id

        public string T01102 { get; set; } // team name

        public DateTime T01103 { get; set; } // Date when team is created

        public string T01104 { get; set; } = string.Empty; // Name of member who created name
        // for team creator name we need to mdifiy POCO to DTO convertion
    }
}