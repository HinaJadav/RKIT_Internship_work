using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTOs
{
    public class DTOLogin
    {
        /// <summary>
        /// Username of the user
        /// </summary>
        [Required(ErrorMessage = "U01F02 (Username) is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "U01F02 (Username) must be between 3 and 50 characters.")]
        [JsonProperty("U01F02")]
        public string U01102 { get; set; } = string.Empty;

        /// <summary>
        /// Hashed password storage
        /// </summary>
        [Required(ErrorMessage = "U01F03 (Password) is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "U01F03 (Password) must be between 6 and 100 characters.")]
        [JsonProperty("U01F03")]
        public string U01103 { get; set; } = string.Empty;
    }
}
