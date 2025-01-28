namespace ClassTypes.Models
{
    // Partial class

    /// <summary>
    /// Represents the user's educational information.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Gets or sets the highest level of education completed by the user.
        /// </summary>
        public string HighestEducation { get; set; }

        /// <summary>
        /// Gets or sets the name of the institution where the user studied.
        /// </summary>
        public string InstitutionName { get; set; }

        /// <summary>
        /// Gets or sets the field of study or major of the user.
        /// </summary>
        public string FieldOfStudy { get; set; }
    }
}
