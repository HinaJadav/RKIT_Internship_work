using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object for YMU01 (User)
    /// </summary>
    public class DTOYMU01
    {
        /// <summary>
        /// Unique Identifier for the user
        /// </summary>
        [JsonProperty("U01F01")]
        [Key]
        public int U01101 { get; set; }

        /// <summary>
        /// Username of the user
        /// </summary>
        [Required(ErrorMessage = "U01F02 (Username) is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "U01F02 (Username) must be between 3 and 50 characters.")]
        [JsonProperty("U01F02")]
        public string U01102 { get; set; } = string.Empty;

        /// <summary>
        /// Password of the user
        /// </summary>
        [Required(ErrorMessage = "U01F03 (Password) is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "U01F03 (Password) must be between 6 and 100 characters.")]
        [JsonProperty("U01F03")]
        public string U01103 { get; set; } = string.Empty;

        /// <summary>
        /// Role of the user: Admin, Developer, Tester
        /// </summary>
        [Required(ErrorMessage = "U01F04 (Role) is required.")]
        [RegularExpression("^(Admin|Developer|Tester)$", ErrorMessage = "U01F04 (Role) must be either Admin, Developer, or Tester.")]
        [JsonProperty("U01F04")]
        public string U01104 { get; set; } = string.Empty;
    }
}
