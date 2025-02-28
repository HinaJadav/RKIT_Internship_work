
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using FinalDemo.Models.Enums;

namespace FinalDemo.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object for YMB01 (Bug Tracking)
    /// </summary>
    public class DTOYMB01
    {
        /// <summary>
        /// Unique Identifier for the bug/issue
        /// </summary>
        [JsonProperty("B01F01")]
        [Key]
        public int B01101 { get; set; }

        /// <summary>
        /// Title of the bug or issue
        /// </summary>
        [Required(ErrorMessage = "B01F02 (Title) is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "B01F02 (Title) must be between 3 and 100 characters.")]
        [JsonProperty("B01F02")]
        public string B01102 { get; set; } = string.Empty;

        /// <summary>
        /// Detailed description of the bug or issue
        /// </summary>
        [Required(ErrorMessage = "B01F03 (Description) is required.")]
        [StringLength(500, ErrorMessage = "B01F03 (Description) cannot exceed 500 characters.")]
        [JsonProperty("B01F03")]
        public string B01103 { get; set; } = string.Empty;

        /// <summary>
        /// Status of the issue: Open, In Progress, Resolved, Closed
        /// </summary>
        [Required(ErrorMessage = "B01F04 (Status) is required.")]
        [EnumDataType(typeof(BugStatus), ErrorMessage = "B01F04 (Status) must be a valid BugStatus value.")]
        [JsonProperty("B01F04")]
        public BugStatus B01104 { get; set; }

        /// <summary>
        /// Timestamp when the issue was created (UTC format)
        /// </summary>
        [Required(ErrorMessage = "B01F05 (Created At) is required.")]
        [JsonProperty("B01F05")]
        public DateTime B01105 { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Foreign Key - ID of the user assigned to this issue (nullable)
        /// </summary>
        [Required(ErrorMessage = "B01F06 (Assigned User ID) is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "B01F06 (Assigned User ID) must be a positive integer.")]
        [JsonProperty("B01F06")]
        public int B01106 { get; set; }
    }
}
