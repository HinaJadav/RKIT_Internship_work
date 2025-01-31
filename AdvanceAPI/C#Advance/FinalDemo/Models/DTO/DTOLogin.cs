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
        public string Email { get; set; }

        /// <summary>
        /// User's password.
        /// </summary>
        public string Password { get; set; }
    }
}
