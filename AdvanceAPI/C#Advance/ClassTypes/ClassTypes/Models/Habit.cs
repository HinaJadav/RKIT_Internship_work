using System;

namespace ClassTypes.Models
{
    /// <summary>
    /// Represents a habit that a user is tracking.
    /// Includes information about the habit's name, category, frequency, and start/end dates.
    /// Inherits from the BaseModel class for shared properties and behavior.
    /// </summary>
    public class Habit : BaseModel
    {
        /// <summary>
        /// Gets or sets the name of the habit.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category of the habit (e.g., Health, Education, Fitness).
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the frequency of the habit, represented as the number of times per week.
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// Gets or sets the start date of the habit tracking period.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the habit tracking period.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Displays the details of the habit, including its name and frequency.
        /// Overrides the abstract DisplayInfo method from the BaseModel class.
        /// </summary>
        public override void DisplayInfo()
        {
            Console.WriteLine($"Habit: {Name}, Frequency: {Frequency} times per week");
        }
    }
}
