using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTOs
{
    public class DTOBugResponse
    {
        /// <summary>
        /// Unique Identifier for the bug
        /// </summary>
        [Required(ErrorMessage = "B01F01 (Bug ID) is required.")]
        [JsonProperty("B01F01")]
        public int B01101 { get; set; }

        /// <summary>
        /// Title of the bug
        /// </summary>
        [Required(ErrorMessage = "B01F02 (Title) is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "B01F02 (Title) must be between 3 and 100 characters.")]
        [JsonProperty("B01F02")]
        public string B01102 { get; set; }

        /// <summary>
        /// Detailed description of the bug or issue
        /// </summary>
        [Required(ErrorMessage = "B01F03 (Description) is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "B01F03 (Description) must be between 10 and 500 characters.")]
        [JsonProperty("B01F03")]
        public string B01103 { get; set; }

        /// <summary>
        /// Status of the issue: Open, In Progress, Resolved, Closed
        /// </summary>
        [Required(ErrorMessage = "B01F04 (Status) is required.")]
        [JsonProperty("B01F04")]
        public string B01104 { get; set; }

        /// <summary>
        /// Timestamp when the issue was created (UTC format)
        /// </summary>
        [Required(ErrorMessage = "B01F05 (Created At) is required.")]
        [JsonProperty("B01F05")]
        [CustomValidation(typeof(DTOBugResponse), nameof(ValidateCreatedAt))]
        public DateTime B01105 { get; set; }

        /// <summary>
        /// Username of the assigned user
        /// </summary>
        [JsonProperty("B01F07")]
        public string B01107 { get; set; }

        /// <summary>
        /// Custom validation method to ensure the date is not in the future.
        /// </summary>
        public static ValidationResult ValidateCreatedAt(DateTime createdAt, ValidationContext context)
        {
            if (createdAt > DateTime.UtcNow)
            {
                return new ValidationResult("B01105 (Created At) cannot be a future date.");
            }
            return ValidationResult.Success;
        }
    }
}
