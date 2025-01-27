using System;

namespace ClassTypes.Models
{
    // Abstract class

    /// <summary>
    /// The base class for all models in the application.
    /// Contains common properties like Id, CreatedAt, UpdatedAt,
    /// and enforces derived classes to implement the Display
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets and sets the unique identifier for the model.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the model was created.
        /// Defaults to the current date and time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets and sets the timestamp of when the m  was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Display Information.
        /// The must be implemented my derived classes.
        /// </summary>
        public abstract void DisplayInfo();
    }
}
