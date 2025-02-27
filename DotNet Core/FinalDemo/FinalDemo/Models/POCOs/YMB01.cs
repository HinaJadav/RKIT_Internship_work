using FinalDemo.Models.Enums;
using ServiceStack.DataAnnotations;

namespace FinalDemo.Models.POCOs
{
    public class YMB01
    {
        /// <summary>
        /// Primary Key - Unique Identifier for the entity
        /// </summary>
        [PrimaryKey]
        public int B01F01 { get; set; }

        /// <summary>
        /// Title of the bug or issue
        /// </summary>
        public string B01F02 { get; set; } = string.Empty;

        /// <summary>
        /// Detailed description of the bug or issue
        /// </summary>
        public string B01F03 { get; set; } = string.Empty;

        /// <summary>
        /// Status of the issue: Open, In Progress, Resolved, Closed
        /// </summary>
        public BugStatus B01F04 { get; set; }
        /// <summary>
        /// Timestamp when the issue was created (UTC format)
        /// </summary>
        public DateTime B01F05 { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Foreign Key - ID of the user assigned to this issue (nullable)
        /// </summary>
        public int B01F06 { get; set; }

        /// <summary>
        /// Navigation property - The user assigned to this issue
        /// </summary>
        public YMU01 B01F07 { get; set; }
    }
}
