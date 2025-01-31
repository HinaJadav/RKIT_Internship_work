using System;

namespace FinalDemo.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for member details.
    /// </summary>
    public class DTOYMM01
    {
        /// <summary>
        /// Member ID.
        /// </summary>
        public int M01101 { get; set; }

        /// <summary>
        /// Full name of the member 
        /// </summary>
        public string M01102 { get; set; }

        /// <summary>
        /// Member's email address.
        /// </summary>
        public string M01103 { get; set; }

        /// <summary>
        /// Contact number of the member.
        /// </summary>
        public string M01104 { get; set; }

        /// <summary>
        /// Date when the member joined.
        /// </summary>
        public DateTime M01107 { get; set; }

        /// <summary>
        /// Indicates if the member is active (1 = Active, 0 = Inactive).
        /// </summary>
        public int M01108 { get; set; }

        /// <summary>
        /// Member's password.
        /// </summary>
        public string M01109 { get; set; }
    }
}
