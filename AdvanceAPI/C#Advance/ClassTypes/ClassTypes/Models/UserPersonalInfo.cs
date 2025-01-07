
using System;

namespace ClassTypes.Models
{
    /// <summary>
    /// Represents the user's personal information.
    /// This is part of the partial User class.
    /// </summary>
    public partial class User : BaseModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password for the user's account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Displays the user's personal information.
        /// </summary>

        public override void DisplayInfo()
        {
            Console.WriteLine("\nPersonal Information:");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
        }
    }
}
