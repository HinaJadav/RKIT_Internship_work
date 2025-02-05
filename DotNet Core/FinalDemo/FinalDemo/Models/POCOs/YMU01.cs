namespace FinalDemo.Models.POCOs
{
    public class YMU01
    {
        // AUTO INCREMENTED
        // PRIMARY KEY
        public int U01F01 { get; set; } // userid

        // STRING LENGTH = 20
        public string U01F02 { get; set; } = string.Empty; // user name

        // UNIQUE
        public string U01F03 { get; set; } = string.Empty; // email

        public string U01F04 { get; set; } = string.Empty; // password with length = 8

        // this date will be not update for any CRUD operation
        public DateTime U01F05 { get; set; } = DateTime.Now; // date when user is register 
    }
}
