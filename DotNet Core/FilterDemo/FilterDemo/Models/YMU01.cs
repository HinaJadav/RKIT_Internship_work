namespace FilterDemo.Models
{
    /// <summary>
    /// User general information
    /// </summary>
    public class YMU01
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int U01F01 { get; set; } 

        /// <summary>
        /// User name
        /// </summary>
        public string U01F02 { get; set; } = string.Empty;
        /// <summary>
        /// User password
        /// </summary>
        public string U01F03 { get; set; } = string.Empty;
        /// <summary>
        /// user roles - admin, manager, employee
        /// </summary>
        public string U01F04 { get; set; } = string.Empty;
    }
}
