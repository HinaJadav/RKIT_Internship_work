using System;

namespace ClassTypes.Models
{
    // Static class
    
    /// <summary>
    /// Provides utility methods for common operations, such as email validation 
    /// and streak calculation, used across the application.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Validates whether the provided email address is in a basic valid format.
        /// Checks for the presence of "@" and "." in the email string.
        /// </summary>
        /// <param name="Email">The email address to validate.</param>
        /// <returns>True if the email is valid; otherwise, false.</returns>
        public static bool InValidEmail(string Email)
        {
            return Email.Contains("@") && Email.Contains(".");
        }

        /// <summary>
        /// Calculates the number of days between the given start date and end date.
        /// </summary>
        /// <param name="startDate">The starting date of the period.</param>
        /// <param name="endDate">The ending date of the period.</param>
        /// <returns>The number of days as an integer.</returns>
        public static int CalculateStreak(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days;
        }
    }
}
