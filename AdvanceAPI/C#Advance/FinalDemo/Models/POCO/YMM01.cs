using ServiceStack.DataAnnotations;
using System;

namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// Represents a member entity with personal and account details.
    /// </summary>
    public class YMM01
    {
<<<<<<< HEAD
        [PrimaryKey]
        [AutoIncrement]
        public int M01F01 { get; set; } // id
=======
        /// <summary>
        /// Primary key - Unique member ID.
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int M01F01 { get; set; }
>>>>>>> 0f8054d594a105dfd50cdea410e51bb1e01a5a1a

        /// <summary>
        /// Full name of the member.
        /// </summary>
        [Required] // add into dto 
        [StringLength(50)]
<<<<<<< HEAD
        public string M01F02 { get; set; } // full name


        [Unique]
        public string M01F03 { get; set; } // email

        [DecimalLength(10, 0)] // Precision 10, Scale 0 (whole numbers)
        public decimal M01F04 { get; set; } // contact number

      

        [IgnoreOnUpdate]
        public DateTime M01F07 { get; set; } // joining date

        public bool M01F08 { get; set; } // isActive member or not 
=======
        public string M01F02 { get; set; }
>>>>>>> 0f8054d594a105dfd50cdea410e51bb1e01a5a1a

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
        public bool M01F08 { get; set; }

        /// <summary>
        /// Member's password (Required).
        /// </summary>
        [Required]
<<<<<<< HEAD
        public string M01F09 { get; set; } // member password
=======
        public string M01F09 { get; set; }
>>>>>>> 0f8054d594a105dfd50cdea410e51bb1e01a5a1a
    }
}
