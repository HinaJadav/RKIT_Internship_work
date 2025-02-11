using ServiceStack.DataAnnotations;
using System;

namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// Represents a member entity with personal and account details.
    /// </summary>
    public class YMM01
    {

        /// <summary>
        /// Primary key - Unique member ID.
        /// </summary>
        [PrimaryKey]
        public int M01F01 { get; set; }

       
        /// <summary>
        /// Unique email address of the member.
        /// </summary>
        [Unique]
        public string M01F03 { get; set; }

        /// <summary>
        /// Contact number with a precision of 10 digits.
        /// </summary>
        [DecimalLength(10, 0)]
        public decimal M01F04 { get; set; }

        /// <summary>
        /// Date when the member joined. Cannot be updated.
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime M01F07 { get; set; }

        /// <summary>
        /// Indicates if the member is active.
        /// </summary>
        public int M01F08 { get; set; }

        /// <summary>
        /// Member's password (Required).
        /// </summary>
       

        public string M01F09 { get; set; } // member password

    }
}
