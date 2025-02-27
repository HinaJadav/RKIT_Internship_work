using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTOs
{
    public class DTOResponse
    {
        /// <summary>
        /// Unique Identifier for the user
        /// </summary>
        [Required(ErrorMessage = "U01F01 (User ID) is required.")]
        [JsonProperty("U01F01")]
        public int U01101 { get; set; }

        /// <summary>
        /// Username of the user
        /// </summary>
        [Required(ErrorMessage = "U01F02 (Username) is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "U01F02 (Username) must be between 3 and 50 characters.")]
        [JsonProperty("U01F02")]
        public string U01102 { get; set; } = string.Empty;

        /// <summary>
        /// Role of the user: Admin, Developer, Tester
        /// </summary>
        [Required(ErrorMessage = "U01F04 (Role) is required.")]
        [RegularExpression("^(Admin|Developer|Tester)$", ErrorMessage = "U01F04 (Role) must be either Admin, Developer, or Tester.")]
        [JsonProperty("U01F04")]
        public string U01104 { get; set; } = string.Empty;
    }
}
