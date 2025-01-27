using System;

namespace ClassTypes.Models
{
    // Partial class

    /// <summary>
    /// Represents the user's personal information.
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
        /// Gets or sets the address details of the user.
        /// </summary>
        public AddressDetails Address { get; set; }

        /// <summary>
        /// Displays the user's personal information.
        /// </summary>
        public override void DisplayInfo()
        {
            Console.WriteLine("\nPersonal Information:");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            if (Address != null)
            {
                Console.WriteLine("\nAddress Details:");
                Address.DisplayAddress();
            }
        }

        // Nested class

        /// <summary>
        /// Represents the address details of the user.
        /// </summary>
        public class AddressDetails
        {
            /// <summary>
            /// Gets or sets the street address.
            /// </summary>
            public string Street { get; set; }

            /// <summary>
            /// Gets or sets the city.
            /// </summary>
            public string City { get; set; }

            /// <summary>
            /// Gets or sets the state.
            /// </summary>
            public string State { get; set; }

            /// <summary>
            /// Gets or sets the postal code.
            /// </summary>
            public string PostalCode { get; set; }

            /// <summary>
            /// Displays the address details.
            /// </summary>
            public void DisplayAddress()
            {
                Console.WriteLine($"Street: {Street}");
                Console.WriteLine($"City: {City}");
                Console.WriteLine($"State: {State}");
                Console.WriteLine($"Postal Code: {PostalCode}");
            }
        }
    }
}
