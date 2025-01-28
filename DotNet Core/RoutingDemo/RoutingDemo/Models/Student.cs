namespace RoutingDemo.Models
{
    ///<summary>
    /// A simple Student class to represent student data.
    /// </summary>
    public class Student
    {
        ///<summary>
        /// Gets or sets the student ID.
        /// </summary>
        public int Id { get; set; }

        ///<summary>
        /// Gets or sets the student's first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        ///<summary>
        /// Gets or sets the student's last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        ///<summary>
        /// Gets or sets the student's education information.
        /// </summary>
        public string EducationInfo { get; set; } = string.Empty;
    }
}
