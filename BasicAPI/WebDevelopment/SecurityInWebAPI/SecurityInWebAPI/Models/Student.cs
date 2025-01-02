using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityInWebAPI.Models
{

    /// <summary>
    /// Represents a student in the Student Management System.
    /// </summary>
    public class Student
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the unique identifier for the student's enrollment.
        /// </summary>
        public int EnrollmentID { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the student.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the contact information of the student.
        /// </summary>
        public long ContactInformation { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the student. Nullable.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the student. Default is "Other".
        /// </summary>
        public string Gender { get; set; } = "Other";

        /// <summary>
        /// Gets or sets the address of the student.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the year of graduation for the student.
        /// </summary>
        public int YearOfGraduation { get; set; }

        /// <summary>
        /// Gets or sets the seat type of the student. Default is "GIA".
        /// </summary>
        public string StudentSeatType { get; set; } = "GIA";

        /// <summary>
        /// Gets or sets the fees status of the student. Default is "UNPAID".
        /// </summary>
        public string FeesStatus { get; set; } = "UNPAID";

        /// <summary>
        /// Gets or sets the department ID the student is associated with.
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Gets or sets the active status of the student. Default is 1 (active).
        /// </summary>
        public byte IsActive { get; set; } = 1;

        #endregion

    }
}