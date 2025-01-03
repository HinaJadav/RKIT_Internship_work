namespace APIVersioning_In_WebAPI.Models
{
    /// <summary>
    /// Represents the student data for version 1 of the API.
    /// </summary>
    public class StudentV1Model
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the marks obtained by the student.
        /// </summary>
        public decimal Marks { get; set; }

        #endregion
    }
}
