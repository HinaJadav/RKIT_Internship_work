using System;

namespace StudentMgmtSystem.Model
{
    public class YMS01
    {
        /// <summary>
        /// Original Column: EnrollmentID
        /// </summary>
        public int S01f01 { get; set; }

        /// <summary>
        /// Original Column: Name
        /// </summary>
        public string S01f02 { get; set; }

        /// <summary>
        /// Original Column: Email
        /// </summary>
        public string S01f03 { get; set; }

        /// <summary>
        /// Original Column: ContactInformation
        /// Default value: 'Not Provided'
        /// </summary>
        public string S01f04 { get; set; }

        /// <summary>
        /// Original Column: DateOfBirth
        /// </summary>
        public DateTime? S01f05 { get; set; }

        /// <summary>
        /// Original Column: Gender
        /// Possible values: 'Male', 'Female', 'Other'
        /// Default value: 'Other'
        /// </summary>
        public string S01f06 { get; set; }

        /// <summary>
        /// Original Column: Address
        /// </summary>
        public string S01f07 { get; set; }

        /// <summary>
        /// Original Column: YearOfGraduation
        /// </summary>
        public int? S01f08 { get; set; }

        /// <summary>
        /// Original Column: StudentSeatType
        /// Default value: 'GIA'
        /// </summary>
        public string S01f09 { get; set; }

        /// <summary>
        /// Original Column: FeesStatus
        /// Possible values: 'PAID', 'UNPAID'
        /// Default value: 'UNPAID'
        /// </summary>
        public string S01f10 { get; set; }

        /// <summary>
        /// Original Column: DepartmentID
        /// </summary>
        public int? S01f11 { get; set; }

        /// <summary>
        /// Original Column: IsActive
        /// Default value: 1
        /// </summary>
        public byte? S01f12 { get; set; }
    }
}