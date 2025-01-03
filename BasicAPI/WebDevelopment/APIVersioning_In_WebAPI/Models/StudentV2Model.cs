namespace APIVersioning_In_WebAPI.Models
{
    /// <summary>
    /// Represents the student data for version 2 of the API.
    /// </summary>
    public class StudentV2Model
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the student.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the student.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the contact information of the student.
        /// </summary>
        public string ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the marks obtained by the student.
        /// </summary>
        public decimal Marks { get; set; }

        #endregion
    }
}
