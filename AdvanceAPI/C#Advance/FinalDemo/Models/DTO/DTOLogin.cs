using System.Text.Json.Serialization;

namespace FinalDemo.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for user login credentials.
    /// </summary>
    public class DTOLogin
    {
        /// <summary>
        /// User's email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// User's password.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
