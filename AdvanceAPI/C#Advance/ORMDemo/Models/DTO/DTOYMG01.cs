using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORMDemo.Models.DTO
{
    /// <summary>
    /// DTO for the YMG01 entity.
    /// </summary>
    public class DTOYMG01
    {
        /// <summary>
        /// Gets or sets the G01101 field.
        /// </summary>
        [JsonPropertyName("Game ID: ")]
        [Required(ErrorMessage = "Game id is required.")]
        
        public int G01101 { get; set; }

        /// <summary>
        /// Gets or sets the G01102 field.
        /// </summary>
        [JsonPropertyName("Game name: ")]
        [Required(ErrorMessage = "Game name is required.")]
        [StringLength(50, ErrorMessage = "Game name cannot exceed 50 characters.")]
        public string G01102 { get; set; }

        /// <summary>
        /// Gets or sets the G01103 field.
        /// </summary>
        [JsonPropertyName("No of players: ")]
        [Range(1, 100, ErrorMessage = "No of players must be greater than zero.")]
        public int G01103 { get; set; }
    }
}
