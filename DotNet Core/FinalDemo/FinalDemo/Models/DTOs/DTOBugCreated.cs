using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTOs
{
    public class DTOBugCreated
    {
        /// <summary>
        /// Title of the bug
        /// </summary>
        [Required(ErrorMessage = "B01F02 (Title) is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "B01F02 (Title) must be between 3 and 100 characters.")]
        [JsonProperty("B01F02")]
        public string B01102 { get; set; } = string.Empty;

        /// <summary>
        /// Detailed description of the bug or issue
        /// </summary>
        [Required(ErrorMessage = "B01F03 (Description) is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "B01F03 (Description) must be between 10 and 500 characters.")]
        [JsonProperty("B01F03")]
        public string B01103 { get; set; } = string.Empty;
    }
}