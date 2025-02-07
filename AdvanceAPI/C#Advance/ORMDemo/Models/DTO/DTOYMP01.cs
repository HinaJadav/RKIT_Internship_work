using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORMDemo.Models.DTO
{
    /// <summary>
    /// DTO for the YMP01 entity.
    /// </summary>
    public class DTOYMP01
    {
        /// <summary>
        /// Gets or sets the P01102 field.
        /// </summary>
        [JsonPropertyName("Player Name: ")]
        [Required(ErrorMessage = "Player name is required.")]
        [StringLength(50, ErrorMessage = "Player name cannot exceed 50 characters.")]
        public string P01102 { get; set; }

        /// <summary>
        /// Gets or sets the P01103 field.
        /// </summary>
        [JsonPropertyName("Player email: ")]
        [Required(ErrorMessage = "Player email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string P01103 { get; set; }

        /// <summary>
        /// Gets or sets the P01104 field.
        /// </summary>
        [JsonPropertyName("Team Name: ")]
        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(10, ErrorMessage = "Team name cannot exceed 10 characters.")]
        public string P01104 { get; set; }

        /// <summary>
        /// Gets or sets the P01105 field.
        /// </summary>
        [JsonPropertyName("Game Id: ")]
        [Required(ErrorMessage = "Game id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Game must be greater than zero.")]
        public int P01105 { get; set; }
    }
}
