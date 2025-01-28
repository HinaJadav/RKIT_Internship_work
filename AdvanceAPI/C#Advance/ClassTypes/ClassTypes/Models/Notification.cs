using System;

namespace ClassTypes.Models
{
    //Sealed class

    /// <summary>
    /// Represents a notification sent to a user regarding their habit.
    /// Do not expect it to be inherited.
    /// </summary>
    public sealed class Notification
    {
        /// <summary>
        /// Gets or sets the message content of the notification.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the notification was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the recipient user for the notification.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the status of whether the notification was read by the user.
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Displays the notification details.
        /// </summary>
        public void DisplayNotification()
        {
            Console.WriteLine($"Notification for {UserEmail}: {Message}");
            Console.WriteLine($"Created At: {CreatedAt}");
            Console.WriteLine($"Status: {(IsRead ? "Read" : "Unread")}");
        }
    }
}
