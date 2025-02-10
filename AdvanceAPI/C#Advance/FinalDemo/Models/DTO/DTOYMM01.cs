using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Member
{
    /// <summary>
    /// Member ID.
    /// </summary>
    [JsonPropertyName("member_id")]
    public int M01101 { get; set; }

    /// <summary>
    /// Full name of the member.
    /// </summary>
    [JsonPropertyName("full_name")]
    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
    public string M01102 { get; set; }

    /// <summary>
    /// Member's email address.
    /// </summary>
    [JsonPropertyName("email")]
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(150, ErrorMessage = "Email cannot exceed 150 characters.")]
    public string M01103 { get; set; }

    /// <summary>
    /// Contact number of the member.
    /// </summary>
    [JsonPropertyName("contact_number")]
    [Required(ErrorMessage = "Contact number is required.")]
    [StringLength(15, ErrorMessage = "Contact number cannot exceed 15 characters.")]
    public string M01104 { get; set; }

    /// <summary>
    /// Date when the member joined.
    /// </summary>
    [JsonPropertyName("joined_date")]
    [Required(ErrorMessage = "Joined date is required.")]
    public DateTime M01107 { get; set; }

    /// <summary>
    /// Indicates if the member is active (1 = Active, 0 = Inactive).
    /// </summary>
    [JsonPropertyName("is_active")]
    [Required(ErrorMessage = "Status is required.")]
    [Range(0, 1, ErrorMessage = "Invalid status. Use 1 for Active and 0 for Inactive.")]
    public int M01108 { get; set; }

    /// <summary>
    /// Member's password.
    /// </summary>
    [JsonPropertyName("password")]
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
    public string M01109 { get; set; }
}
